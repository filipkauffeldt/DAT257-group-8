using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using API;
using Client.API;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen.Blazor;


namespace Client.Components
{
    public partial class DashBoard
    {
        public string test;
        private Country? countryOfTheDay;
        private HomeCountryDropDown dropDown;
        private List<String> CountryNames = new List<string> { "Water", "Electricity", "Grub"};
        private List<Country> comparedCountries = new List<Country>();
        private Country? countryOrigin { get; set; }
        private Country countryComp = new Country()
        {
            Name = "Bulgaria",
            Code = "BU",
            Data = [new Data() { Name = "Water", Unit = "L", Points = [new DataPoint() { Date = new DateOnly(2023,1,1), Value = 50}] }, new Data() { Name = "Electricity", Unit = "W", Points = [new DataPoint() { Date = new DateOnly(2023, 1, 1), Value = 50 }] }]
        };
        private Country countryCompTwo = new Country()
        {
            Name = "BOgusland",
            Code = "BO",
            Data = [new Data() { Name = "Water", Unit = "L", Points = [new DataPoint() { Date = new DateOnly(2023, 1, 1), Value = 200 }] }, new Data() { Name = "Electricity", Unit = "W", Points = [new DataPoint() { Date = new DateOnly(2023, 1, 1), Value = 30 }] }]
        };
        DateOnly dateOnlyLol = new DateOnly(2023, 1, 1);
        // Dummy labels
        private readonly string[] dataLabels = { "Water", "Electricity", "CO2" };

        protected override async Task OnInitializedAsync()
        {
            countryOfTheDay = await apiHandler.FetchCountryOfTheDay(httpClient);
            var homeCountry = await apiHandler.FetchCountry("swe", httpClient);
            comparedCountries.Add(homeCountry);
            countryOrigin = new Country()
            {
                Name = "Bulgaria",
                Code = "BU",
                Data = [new Data() { Name = "Water", Unit = "L", Points = [new DataPoint() { Date = new DateOnly(2023, 1, 1), Value = 100 }] }, new Data() { Name = "Electricity", Unit = "W", Points = [new DataPoint() { Date = new DateOnly(2023, 1, 1), Value = 50 }] }]
            };
        }

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
                    Data = [new Data() { Name = "Water", Unit = "L", Points = [new DataPoint() { Date = new DateOnly(2023, 1, 1), Value = 31 }] }, new Data() { Name = "Electricity", Unit = "W", Points = [new DataPoint() { Date = new DateOnly(2023, 1, 1), Value = 22 }] }]
                };
            }
            StateHasChanged();
        }
    }
}
