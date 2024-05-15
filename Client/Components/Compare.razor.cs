using API;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Client.Store.Actions;
using Client.Store.States;
using System.Reflection;
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

        private IDictionary<string, string> _availableCountries = new Dictionary<string, string>();

        private DateOnly _date = new DateOnly(2022, 1, 1);

        private IList<Country> _countries = new List<Country>();
        private int _comparisonChangedIndex = 0;

        protected override async Task OnInitializedAsync()
        {
            await InitCompareCountryNamesAsync();
            foreach (Country country in State.Value.CountryIdentifiers)
            {
                _availableCountries.Add(country.Name, country.Code);
            }
            await InitOriginCountryAsync();
            //await InitComparedCountryAsync();
            await InitComparedCountriesAsync();
            InitSharedMetrics();
            InitShownMetrics();
            UpdateCountryList();
            _initialized = true;
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

        private async Task InitComparedCountryAsync()
        {
            if (State.Value.ComparedCountries[0] != null) return;
            var country = await _apiHandler.FetchCountryOfTheDayAsync(_httpClient);
            Dispatcher.Dispatch(new ComparedCountryChosenAction(country));
        }

        private async Task InitComparedCountriesAsync()
        {
            if (State.Value.ComparedCountries != null) return;
            var country = await _apiHandler.FetchCountryOfTheDayAsync(_httpClient);
            Dispatcher.Dispatch(new InitComparedCountriesAction(country));
            //Dispatcher.Dispatch(new ComparedCountriesChosenAction(country, 0));
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
            //var countryCompared = State.Value.ComparedCountries[0];
            var countriesCompared = State.Value.ComparedCountries;



            if (countryOrigin == null || countryOrigin.Data == null ||
                countriesCompared == null || countriesCompared.Any(d => d == null) ||countriesCompared.Any(d => d.Data == null)) 
            {
                return new List<string>();
            }

            var sharedMetrics = new List<string>();
            var countryCompDataList = new List<Boolean>(Enumerable.Repeat(false, countriesCompared.Count()));
            foreach (var metric in countryOrigin.Data.Select(d => d.Name).ToList())
            {
                var countryCompDataExists = countryOrigin.Data?.Any(d => d.Name == metric && d.Points.Any(e => e.Date.Year == _date.Year)) ?? false;
                var i = 0;
                foreach (var country in countriesCompared)
                {
                    countryCompDataList[i] = country.Data?.Any(d => d.Name == metric && d.Points.Any(e => e.Date.Year == _date.Year)) ?? false;
                    i++;
                }
                //var countryCompTwoDataExists = countryCompared.Data?.Any(d => d.Name == metric && d.Points.Any(e => e.Date.Year == _date.Year)) ?? false;

                if (countryCompDataExists && !countryCompDataList.Any(c => c == false))
                {
                    sharedMetrics.Add(metric);
                }
            }
            return sharedMetrics;
        }
        private async void UpdateOriginCountry(string name)
        {
            var country = await _apiHandler.FetchCountryByYearAsync(_httpClient, _availableCountries[name], _date);
            Dispatcher.Dispatch(new OriginCountryChosenAction(country));
            var sharedMetrics = GetSharedMetrics();
            UpdateSharedMetrics(sharedMetrics);
            UpdateShownMetrics(sharedMetrics);
            UpdateCountryList();
            StateHasChanged();
        }

        private async void UpdateComparedCountry(string name, int index)
        {
            var countries = State.Value.ComparedCountries;
            var country = await _apiHandler.FetchCountryByYearAsync(_httpClient, _availableCountries[name], _date);
            countries[index] = country;
            _comparisonChangedIndex = index;
            Dispatcher.Dispatch(new ComparedCountriesChosenAction(countries));
            var sharedMetrics = GetSharedMetrics();
            UpdateShownMetrics(sharedMetrics);
            UpdateSharedMetrics(sharedMetrics);
            UpdateCountryList();
            StateHasChanged();
        }

        private async void AddComparedCountry()
        {
            var countries = State.Value.ComparedCountries;
            if(countries.Count < 3)
            {
                var country = await _apiHandler.FetchCountryOfTheDayAsync(_httpClient);
                countries.Add(country);
                Dispatcher.Dispatch(new ComparedCountriesChosenAction(countries));
                var sharedMetrics = GetSharedMetrics();
                UpdateShownMetrics(sharedMetrics);
                UpdateSharedMetrics(sharedMetrics);
                StateHasChanged();
            }
        }

        private void RemoveComparedCountry()
        {
            var countries = State.Value.ComparedCountries;
            if(countries.Count > 1)
            {
                var lastIndex = countries.Count - 1;
                if(_comparisonChangedIndex == lastIndex)
                {
                    _comparisonChangedIndex = 0;
                }
                countries.RemoveAt(lastIndex);
                Dispatcher.Dispatch(new ComparedCountriesChosenAction(countries));
                var sharedMetrics = GetSharedMetrics();
                UpdateShownMetrics(sharedMetrics);
                UpdateSharedMetrics(sharedMetrics);
                StateHasChanged();
            }
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

        // Trigger re-rendering of the ComparisonComponents
        private void UpdateCountryList()
        {
            var newCountryList = new List<Country>();
            newCountryList.Add(State.Value.ComparedCountry);
            _countries = newCountryList;
        }
    }
}