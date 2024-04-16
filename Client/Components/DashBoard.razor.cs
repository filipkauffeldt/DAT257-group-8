using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;
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
            Code = "BU",
            Data = [new Data() { Name = "Water", Unit = "L", Points = [new DataPoint() { Date = new DateOnly(2023,1,1), Value = 100}] },
                    new Data() {Name = "CO2 Emissions", Unit = "Kg", Points = [new DataPoint() { Date = new DateOnly(2023, 1, 1), Value = 200 }]}]
        };
        private Country countryCompTwo = new Country()
        {
            Name = "BOgusland",
            Code = "BO",
            Data = [new Data() { Name = "Water", Unit = "L", Points = [new DataPoint() { Date = new DateOnly(2023, 1, 1), Value = 200 }] },
                    new Data() {Name = "CO2 Emissions", Unit = "Kg", Points = [new DataPoint() { Date = new DateOnly(2023, 1, 1), Value = 200 }]}]
        };
        DateOnly date = new DateOnly(2023, 1, 1);
        // Dummy labels
        private readonly string[] dataLabels = { "Water", "Electricity", "CO2" };
        private IEnumerable<string> dataMetrics = new List<string>();

        protected override async Task OnInitializedAsync()
        {
            //countryOfTheDay = await apiHandler.FetchCountryOfTheDay(httpClient);
            //var homeCountry = await apiHandler.FetchCountry("swe", httpClient);
            var minDate = new DateOnly(2022, 1, 1);
            var maxDate = new DateOnly(2024, 1, 1);
            countryComp = await apiHandler.FetchCountryDataByTimeSpan(httpClient, countryComp.Code, minDate, maxDate);
            countryCompTwo = await apiHandler.FetchCountryDataByTimeSpan(httpClient, countryCompTwo.Code, minDate, maxDate);
            //comparedCountries.Add(homeCountry);
            dataMetrics = GetSharedMetrics();
        }

        private IEnumerable<string> GetSharedMetrics()
        {
            if (countryComp == null || countryComp.Data == null ||
                countryCompTwo == null || countryCompTwo.Data == null) {
                return Enumerable.Empty<string>();
            }

            var sharedMetrics = countryComp.Data
                .Select(data => data.Name)
                .Intersect(countryCompTwo.Data.Select(data => data.Name));

            return sharedMetrics;
        }

        private static string FormatLabel(string label)
        {
            return $"{label.Replace(" ", "-")}-statistic";
        }
    }
}
