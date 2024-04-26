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
        private HttpClient httpClient { get; set; }

        [Inject]
        private IApiHandler apiHandler { get; set; }

        private string selectedHome = "";

        private string selectedOther = "";

        private Country _country { get; set; }
        
        private Country _countryToCompareWith { get; set; }

        private IDictionary<string, string> availableCountries = new Dictionary<string, string>();
        
        private DateOnly _date = new DateOnly(2022, 1, 1);
        private IList<string> _dataMetrics = new List<string>();

        private IList<string> _availableMetrics = new List<string>();

        protected override async Task OnInitializedAsync()
        {
            var countries = await apiHandler.FetchAllCountryIdentifiers(httpClient);
            foreach (Country country in countries)
            {
                availableCountries.Add(country.Name, country.Code);
            }
            _country = await apiHandler.FetchHomeCountry(httpClient);
            _countryToCompareWith = await apiHandler.FetchCountryOfTheDay(httpClient);
            selectedHome = _country.Name;
            selectedOther = _countryToCompareWith.Name;
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
            selectedHome = name;
            _country = await apiHandler.FetchCountryByYear(httpClient, availableCountries[name], _date);
            _dataMetrics = GetValidMetrics();
            StateHasChanged();
        }

        private async void UpdateOtherCountry(string name)
        {
            selectedOther = name;
            _countryToCompareWith = await apiHandler.FetchCountryByYear(httpClient, availableCountries[name], _date);
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

