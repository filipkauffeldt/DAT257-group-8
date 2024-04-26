using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using API;
using Client.API;
using Microsoft.AspNetCore.Components;

namespace Client.Components
{
    public partial class DashBoard
    {
        private HomeCountryDropDown dropDown;
        private Dictionary<string, ComparisonComponent> compComp = new();
        private Dictionary<string, string> countryCodeDict = new();
        private List<string> countryNames;

        [Inject]
        private HttpClient httpClient { get; set; }

        [Inject]
        private IApiHandler apiHandler { get; set; }

        private Country? _country;

        private Country? _countryToCompareWith;
        
        private DateOnly _date = new DateOnly(2022, 1, 1);
        private IList<string> _dataMetrics = new List<string>();
        private IList<string> _availableMetrics = new List<string>();
        private bool _homeCountryError = false;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                _country = await apiHandler.FetchHomeCountryAsync(httpClient);
                _homeCountryError = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _homeCountryError = true;
            }
            _countryToCompareWith = await apiHandler.FetchCountryOfTheDayAsync(httpClient);
            _dataMetrics = GetValidMetrics();
            _availableMetrics = _dataMetrics;
            var countryIdentifiers = await apiHandler.FetchAllCountryIdentifiers(httpClient);
            foreach(var country in countryIdentifiers)
            {
                countryCodeDict.Add(country.Name, country.Code);
            }
            countryNames = countryCodeDict.Keys.ToList();
        }

        private async void HomeCountryChange(string CountryCode)
        {
            _country = await apiHandler.FetchCountryByYear(httpClient, CountryCode, _date);
            StateHasChanged();
            foreach (var cC in compComp.Values)
            {
                cC.LoadValues();
            }
            StateHasChanged();
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
