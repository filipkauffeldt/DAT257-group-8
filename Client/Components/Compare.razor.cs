﻿using API;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Client.Store.Actions;
using Client.Store.States;
using System.Reflection;
using System.Linq.Dynamic.Core.Exceptions;
using System;
using System.Collections.Generic;

namespace Client.Components
{
    public partial class Compare
    {
        [Inject]
        private HttpClient _httpClient { get; set; }

        [Inject]
        private IApiHandler _apiHandler { get; set; }

        [Inject]
        private IDispatcher Dispatcher { get; set; }

        [Inject]
        private IState<CustomCompareState> State { get; set; }

        private bool _initialized = false;

        private IDictionary<string, string> _nameToCodeMap = new Dictionary<string, string>();

        private DateOnly _date = new DateOnly(2022, 1, 1);

        private List<string> ListOfYears = new List<string>();

        private string SelectedYear;
        protected override async Task OnInitializedAsync()
        {
            _date = State.Value.Year;
            SelectedYear = _date.Year.ToString();
            PopulateListOfYears();
            await InitCompareCountryNamesAsync();
            foreach (Country country in State.Value.CountryIdentifiers)
            {
                _nameToCodeMap.Add(country.Name, country.Code);
            }
            await InitOriginCountryAsync();
            await InitComparedCountriesAsync();
            InitSharedMetrics();
            InitShownMetrics();
            _initialized = true;
            
        }

        private async void PopulateListOfYears()
        {
            for(int i = DateTime.Today.Year; i >= 1950; i--)
            {
                ListOfYears.Add(i.ToString());
            }
        }
        private async void UpdateCountriesBasedOnYear(string year)
        {
            int _year;
            if ( !int.TryParse(year, out _year)) { throw new ParseException("Could not parse string year to int", 1); }
            SelectedYear = year;
            DateOnly date = new DateOnly(_year,1,1);
            _date = date;

            UpdateOriginCountryAsync(State.Value.OriginCountry.Name);
            RefreshCompCountriesBasedOnYear(State.Value.ComparedCountries);
            Dispatcher.Dispatch(new UpdateYearAction(date));
            StateHasChanged();
        }

        private async void RefreshCompCountriesBasedOnYear(IList<Country> countries)
        {
            for (int i = 0; i < countries.Count; i++) 
            {
                countries[i] = await _apiHandler.FetchCountryByYearAsync(_httpClient, _nameToCodeMap[countries[i].Name], _date);
            }
            UpdateComparedCountries(countries);
        }

        private async Task InitCompareCountryNamesAsync() {
            if (State.Value.CountryIdentifiers != null) return;
            var countries = await _apiHandler.FetchAllCountryIdentifiersAsync(_httpClient);
            Dispatcher.Dispatch(new CountryIdentifiersFetchedAction(countries.ToList()));
        }

        private async Task InitOriginCountryAsync()
        {
            if (State.Value.OriginCountry != null) return;
            var country = await _apiHandler.FetchHomeCountryAsync(_httpClient);
            Dispatcher.Dispatch(new OriginCountryChosenAction(country));
        }

        private async Task InitComparedCountriesAsync()
        {
            if (State.Value.ComparedCountries != null) return;
            var country = await _apiHandler.FetchCountryOfTheDayAsync(_httpClient);
            Dispatcher.Dispatch(new ComparedCountriesChosenAction([country]));
        }

        private void UpdateSharedMetrics(IList<string> metrics) {
            Dispatcher.Dispatch(new ComparedSharedMetricsChangedAction(metrics));	
        }

       
        private void UpdateShownMetrics(IList<string> metrics)
        {
            Dispatcher.Dispatch(new ComparedMetricsSelectedAction(metrics));
        }

        private void InitSharedMetrics() {
            if (State.Value.SharedMetrics.Count > 0) return;
            var sharedMetrics = GetSharedMetrics();
            Dispatcher.Dispatch(new ComparedSharedMetricsChangedAction(sharedMetrics));
        }

        private void InitShownMetrics() {
            if (State.Value.ShownMetrics.Count > 0) return;
            Dispatcher.Dispatch(new ComparedMetricsSelectedAction(State.Value.SharedMetrics));
        }

        private IList<string> GetSharedMetrics()
        {
            var countryOrigin = State.Value.OriginCountry;
            var countriesCompared = State.Value.ComparedCountries;

            if (countryOrigin == null || countryOrigin.Data == null ||
                countriesCompared == null || countriesCompared.Any(d => d == null) ||countriesCompared.Any(d => d.Data == null)) 
            {
                return new List<string>();
            }

            var sharedMetrics = new List<string>();
            var countryCompDataList = new List<bool>(Enumerable.Repeat(false, countriesCompared.Count()));
            foreach (var metric in countryOrigin.Data.Select(d => d.Name).ToList())
            {
                var countryCompDataExists = countryOrigin.Data?.Any(d => d.Name == metric && d.Points.Any(e => e.Date.Year == _date.Year)) ?? false;
                var i = 0;
                foreach (var country in countriesCompared)
                {
                    countryCompDataList[i] = country.Data?.Any(d => d.Name == metric && d.Points.Any(e => e.Date.Year == _date.Year)) ?? false;
                    i++;
                }
                if (countryCompDataExists && !countryCompDataList.Any(c => c == false))
                {
                    sharedMetrics.Add(metric);
                }
            }
            return sharedMetrics;
        }
        private async void UpdateOriginCountryAsync(string name)
        {
            var country = await _apiHandler.FetchCountryByYearAsync(_httpClient, _nameToCodeMap[name], _date);
            Dispatcher.Dispatch(new OriginCountryChosenAction(country));
            var sharedMetrics = GetSharedMetrics();
            UpdateShownMetrics(sharedMetrics);
            UpdateSharedMetrics(sharedMetrics);
            StateHasChanged();
        }

        private void UpdateComparedCountries(IList<Country> countries)
        {
            Dispatcher.Dispatch(new ComparedCountriesChosenAction(countries));
            var sharedMetrics = GetSharedMetrics();
            UpdateShownMetrics(sharedMetrics);
            UpdateSharedMetrics(sharedMetrics);
            StateHasChanged();
        }
      
        private static string FormatLabel(string label)
        {
            return $"{label.Replace(" ", "-")}-statistic";
        }

        private void HandleFilterChange(IEnumerable<string> selectedValues)
        {
            var shownMetrics = selectedValues.ToList();

            // Makes sure that when you select unselected values, they end up in the same order as before
            shownMetrics = shownMetrics.OrderBy(d => State.Value.SharedMetrics.IndexOf(d)).ToList();
            UpdateShownMetrics(shownMetrics);
        }

        private async Task HandleCountryChange(IEnumerable<string> selectedCountries)
        {
            var newShownCountries = new List<Country>(State.Value.ComparedCountries);

            // If country is added, fetch it and add it to the list
            if (selectedCountries.Count() > State.Value.ComparedCountries.Count)
            {
                var addedCountryName = selectedCountries.Except(State.Value.ComparedCountries.Select(d => d.Name)).FirstOrDefault();
                var addedCountry = await _apiHandler.FetchCountryByYearAsync(_httpClient, _nameToCodeMap[addedCountryName], _date);
                newShownCountries.Add(addedCountry);
            }
            // If country is removed, remove it from the list
            else
            {
                var removedCountry = State.Value.ComparedCountries.Select(d => d.Name).Except(selectedCountries).FirstOrDefault();
                newShownCountries = newShownCountries.Where(d => d.Name != removedCountry).ToList();
            }
            UpdateComparedCountries(newShownCountries);
        }
    }
}