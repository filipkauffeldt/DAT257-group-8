using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using API;
using Client.API;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen.Blazor;


namespace Client.Components
{
    public partial class DashBoard
    {
        private Country? countryOfTheDay;
        private List<Country> comparedCountries = new List<Country>();
        private Country countryComp = new Country()
        {
            Name = "Bulgaria",
            Code = "BGR",
            Data = [new Data() { Name = "Water", Unit = "L", Points = [new DataPoint() { Date = new DateOnly(2023,1,1), Value = 100}] },
                    new Data() {Name = "CO2 Emissions", Unit = "Kg", Points = [new DataPoint() { Date = new DateOnly(2023, 1, 1), Value = 200 }]}]
        };
        private Country countryCompTwo = new Country()
        {
            Name = "Sweden",
            Code = "SWE",
            Data = [new Data() { Name = "Water", Unit = "L", Points = [new DataPoint() { Date = new DateOnly(2023, 1, 1), Value = 200 }] },
                    new Data() {Name = "CO2 Emissions", Unit = "Kg", Points = [new DataPoint() { Date = new DateOnly(2023, 1, 1), Value = 200 }]}]
        };
        DateOnly date = new DateOnly(2020, 1, 1);
        // Dummy labels
        private readonly string[] dataLabels = { "Water", "Electricity", "CO2" };
        private IList<string> dataMetrics = new List<string>();

        protected override async Task OnInitializedAsync()
        {
            countryComp = await apiHandler.FetchCountryByYear(httpClient, countryComp.Code, date);
            countryCompTwo = await apiHandler.FetchCountryByYear(httpClient, countryCompTwo.Code, date);
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
