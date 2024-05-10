using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using API;
using Client.API;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Client.Store.Actions;
using Client.Store.States;
using System.Reflection;

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

        private string _selectedHome = "";

        private string _selectedOther = "";

        private Country _country;
        
        private Country _countryToCompareWith;

        private IDictionary<string, string> _availableCountries = new Dictionary<string, string>();
        
        private DateOnly _date = new DateOnly(2022, 1, 1);
        private IList<string> _dataMetrics = new List<string>();

        private IList<string> _availableMetrics = new List<string>();

        protected override async Task OnInitializedAsync()
        {
            await InitCompareCountryNamesAsync();
            foreach (Country country in State.Value.CountryIdentifiers)
            {
                _availableCountries.Add(country.Name, country.Code);
            }
            await InitOriginCountryAsync();
            // _country = State.Value.OriginCountry;
            await InitComparedCountryAsync();
            // _countryToCompareWith = await _apiHandler.FetchCountryOfTheDayAsync(_httpClient);
            // _countryToCompareWith = State.Value.ComparedCountry;
            // _selectedHome = State.Value.OriginCountry.Name;
            // _selectedOther = State.Value.ComparedCountry.Name;
            // _dataMetrics = GetSharedMetrics();
            InitSharedMetrics();
            _dataMetrics = State.Value.SharedMetrics;
            // _availableMetrics = new List<string>(_dataMetrics);
            UpdateShownMetrics(State.Value.SharedMetrics);
            _availableMetrics = State.Value.ShownMetrics;

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
            if (State.Value.ComparedCountry != null) return;
            var country = await _apiHandler.FetchCountryOfTheDayAsync(_httpClient);
            Dispatcher.Dispatch(new ComparedCountryChosenAction(country));
        }

        private void UpdateShownMetrics(IList<string> metrics)
        {
            Dispatcher.Dispatch(new ComparedMetricsSelectedAction(metrics));
        }

        private void InitSharedMetrics() {
            if (State.Value.SharedMetrics.Count > 0) return;
            var sharedMetrics = GetSharedMetrics();
            Dispatcher.Dispatch(new CustomSharedMetricsDetectedSuccessfullyAction(sharedMetrics));
        }

        private IList<string> GetSharedMetrics()
        {
            var countryOrigin = State.Value.OriginCountry;
            var countryCompared = State.Value.ComparedCountry;

            if (countryOrigin == null || countryOrigin.Data == null ||
                countryCompared == null || countryCompared.Data == null) {
                return new List<string>();
            }

            var sharedMetrics = new List<string>();
            foreach (var metric in countryOrigin.Data.Select(d => d.Name).ToList())
            {
                var countryCompDataExists = countryOrigin.Data?.Any(d => d.Name == metric && d.Points.Any(e => e.Date.Year == _date.Year)) ?? false;
                var countryCompTwoDataExists = countryCompared.Data?.Any(d => d.Name == metric && d.Points.Any(e => e.Date.Year == _date.Year)) ?? false;

                if (countryCompDataExists && countryCompTwoDataExists)
                {
                    sharedMetrics.Add(metric);
                }
            }
            return sharedMetrics;
        }
        private async void UpdateOriginCountry(string name)
        {
            var country = await _apiHandler.FetchCountryByYearAsync(_httpClient, _availableCountries[name], _date);
            Dispatcher.Dispatch(new UpdateOriginCountryAction(country));
            var sharedMetrics = GetSharedMetrics();
            Dispatcher.Dispatch(new CustomSharedMetricsDetectedSuccessfullyAction(sharedMetrics));
            var shownMetrics = new List<string>(_dataMetrics);
            Dispatcher.Dispatch(new ComparedMetricsSelectedAction(shownMetrics));
            StateHasChanged();
        }

        private async void UpdateComparedCountry(string name)
        {
            var country = await _apiHandler.FetchCountryByYearAsync(_httpClient, _availableCountries[name], _date);
            Dispatcher.Dispatch(new UpdateComparedCountryAction(country));
            var sharedMetrics = GetSharedMetrics();
            Dispatcher.Dispatch(new CustomSharedMetricsDetectedSuccessfullyAction(sharedMetrics));
            var shownMetrics = new List<string>(sharedMetrics);
            Dispatcher.Dispatch(new ComparedMetricsSelectedAction(shownMetrics));
            StateHasChanged();
        }

        private static string FormatLabel(string label)
        {
            return $"{label.Replace(" ", "-")}-statistic";
        }

        private void HandleFilterChange(IEnumerable<string> selectedValues)
        {
            _dataMetrics = selectedValues.ToList();

            // Makes sure that when you select unselected values, they end up in the same order as before
            _dataMetrics = _dataMetrics.OrderBy(d => _availableMetrics.IndexOf(d)).ToList();
        }
    }
}

