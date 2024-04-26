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

        private Country countryComp { get; set; }
        
        private Country countryCompTwo { get; set; }
        
        DateOnly date = new DateOnly(2022, 1, 1);
        private IList<string> dataMetrics = new List<string>();

        protected override async Task OnInitializedAsync()
        {
            //countryComp = await apiHandler.FetchCountryByYear(httpClient, "SWE", date); // Sweden
            countryComp = await apiHandler.FetchCountryOfTheDay(httpClient);
            countryCompTwo = await apiHandler.FetchCountryByYear(httpClient, "SWE", date); // Bulgaria
            dataMetrics = GetValidMetrics();
        }

        private IList<string> GetValidMetrics()
        {
            if (countryComp == null || countryComp.Data == null ||
                countryCompTwo == null || countryCompTwo.Data == null) {
                return new List<string>();
            }

            var validMetrics = new List<string>();
            foreach (var metric in countryComp.Data.Select(d => d.Name).ToList())
            {
                var countryCompDataExists = countryComp.Data?.Any(d => d.Name == metric && d.Points.Any()) ?? false;
                var countryCompTwoDataExists = countryCompTwo.Data?.Any(d => d.Name == metric && d.Points.Any()) ?? false;

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
    }
}
