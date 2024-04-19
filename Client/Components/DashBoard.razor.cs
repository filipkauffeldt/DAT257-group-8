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
        public string test;
        private HomeCountryDropDown dropDown;
        private ComparisonComponent comparisonComponent;

        private async void HomeCountryChange(string CountryCode)
        {
            //comparedCountries.RemoveAt(0);
            //var homeCountry = await apiHandler.FetchCountry(Country, httpClient);
            //comparedCountries.Insert(0, homeCountry);
            //countryCompTwo = await apiHandler.FetchCountry(CountryCode, httpClient);
            test = CountryCode;
            if(CountryCode == "swe")
            {
                countryCompTwo = new Country()
                {
                    Name = "Sweden",
                    Code = "swe",
                    Data = [new Data() { Name = "Water", Unit = "L", Points = [new DataPoint() { Date = new DateOnly(2023, 1, 1), Value = 20 }] }, new Data() { Name = "Electricity", Unit = "W", Points = [new DataPoint() { Date = new DateOnly(2023, 1, 1), Value = 22 }] }]
                };
            }
            else if(CountryCode == "den")
            {
                countryCompTwo = new Country()
                {
                    Name = "Denmark",
                    Code = "den",
                    Data = [new Data() { Name = "Water", Unit = "L", Points = [new DataPoint() { Date = new DateOnly(2023, 1, 1), Value = 15 }] }, new Data() { Name = "Electricity", Unit = "W", Points = [new DataPoint() { Date = new DateOnly(2023, 1, 1), Value = 22 }] }]
                };
            }
            else if(CountryCode == "no")
            {
                countryCompTwo = new Country()
                {
                    Name = "Norway",
                    Code = "no",
                    Data = [new Data() { Name = "Water", Unit = "L", Points = [new DataPoint() { Date = new DateOnly(2023, 1, 1), Value = 301 }] }, new Data() { Name = "Electricity", Unit = "W", Points = [new DataPoint() { Date = new DateOnly(2023, 1, 1), Value = 22 }] }]
                };
            }
            StateHasChanged();
            comparisonComponent.LoadValues();
            /*foreach (var comparisonComponent in comparisonComponents)
            {
                comparisonComponent.LoadValues();
            }*/
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
            countryComp = await apiHandler.FetchCountryByYear(httpClient, "SWE", date); // Sweden
            countryCompTwo = await apiHandler.FetchCountryByYear(httpClient, "BGR", date); // Bulgaria
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
