using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using API;
using Client.API;
using Microsoft.AspNetCore.Components;

namespace Client.Components
{
    public partial class DashBoard
    {
        [Inject]
        private HttpClient httpClient { get; set; }

        [Inject]
        private IApiHandler apiHandler { get; set; }

        private Country country { get; set; }
        
        private Country countryToCompareWith { get; set; }
        
        DateOnly date = new DateOnly(2022, 1, 1);
        private IList<string> _dataMetrics = new List<string>();
        private IList<string> _availableMetrics = new List<string>();

        protected override async Task OnInitializedAsync()
        {
            country = await apiHandler.FetchCountryByYear(httpClient, "SWE", date); // Sweden
            countryToCompareWith = await apiHandler.FetchCountryByYear(httpClient, "BGR", date); // Bulgaria
            _dataMetrics = GetValidMetrics();
            _availableMetrics = _dataMetrics;
        }

        private IList<string> GetValidMetrics()
        {
            if (country == null || country.Data == null ||
                countryToCompareWith == null || countryToCompareWith.Data == null) {
                return new List<string>();
            }

            var validMetrics = new List<string>();
            foreach (var metric in country.Data.Select(d => d.Name).ToList())
            {
                var countryCompDataExists = country.Data?.Any(d => d.Name == metric && d.Points.Any()) ?? false;
                var countryCompTwoDataExists = countryToCompareWith.Data?.Any(d => d.Name == metric && d.Points.Any()) ?? false;

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
