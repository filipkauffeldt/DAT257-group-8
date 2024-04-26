using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using API;
using Client.API;
using Microsoft.AspNetCore.Components;

namespace Client.Components
{
    public partial class Custom
    {
        [Inject]
        private HttpClient _httpClient { get; set; }

        [Inject]
        private IApiHandler _apiHandler { get; set; }

        private string _selectedHome = "";

        private string _selectedOther = "";

        private Country _country { get; set; }
        
        private Country _countryToCompareWith { get; set; }

        private IDictionary<string, string> _availableCountries = new Dictionary<string, string>();
        
        private DateOnly _date = new DateOnly(2022, 1, 1);
        private IList<string> _dataMetrics = new List<string>();

        private IList<string> _availableMetrics = new List<string>();

        protected override async Task OnInitializedAsync()
        {
            var countries = await _apiHandler.FetchAllCountryIdentifiers(_httpClient);
            foreach (Country country in countries)
            {
                _availableCountries.Add(country.Name, country.Code);
            }
            _country = await _apiHandler.FetchHomeCountry(_httpClient);
            _countryToCompareWith = await _apiHandler.FetchCountryOfTheDay(_httpClient);
            _selectedHome = _country.Name;
            _selectedOther = _countryToCompareWith.Name;
            _dataMetrics = GetValidMetrics();
            _availableMetrics = _dataMetrics;
        }

        private IList<string> GetValidMetrics()
        {
            if (_country == null || _country.Data == null ||
                _countryToCompareWith == null || _countryToCompareWith.Data == null) {
                return new List<string>();
            }

            var validMetrics = new List<string>();
            foreach (var metric in _country.Data.Select(d => d.Name).ToList())
            {
                var countryCompDataExists = _country.Data?.Any(d => d.Name == metric && d.Points.Any()) ?? false;
                var countryCompTwoDataExists = _countryToCompareWith.Data?.Any(d => d.Name == metric && d.Points.Any()) ?? false;

                if (countryCompDataExists && countryCompTwoDataExists)
                {
                    validMetrics.Add(metric);
                }
            }

            return validMetrics;
        }

        private async void UpdateHomeCountry(string name)
        {
            _selectedHome = name;
            _country = await _apiHandler.FetchCountryByYear(_httpClient, _availableCountries[name], _date);
            _dataMetrics = GetValidMetrics();
            StateHasChanged();
        }

        private async void UpdateOtherCountry(string name)
        {
            _selectedOther = name;
            _countryToCompareWith = await _apiHandler.FetchCountryByYear(_httpClient, _availableCountries[name], _date);
            _dataMetrics = GetValidMetrics();
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

