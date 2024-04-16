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
    public partial class Custom
    {
        private List<string> availableCountries = new List<string>() { "SE", "NO", "DK"};

        private string selectedHome = "SE";
        private string selectedOther = "NO";

        private Country homeCountry = new Country()
        {
            Name = "Sweden",
            Code = "SE",
            Data = [new Data() { Name = "Water", Unit = "L", Points = [new DataPoint() { Date = new DateOnly(2023, 1, 1), Value = 100 }] }]
        };

        private Country otherCountry = new Country()
        {
            Name = "Norway",
            Code = "NO",
            Data = [new Data() { Name = "Water", Unit = "L", Points = [new DataPoint() { Date = new DateOnly(2023, 1, 1), Value = 200 }] }]
        };

        private Dictionary<String, Country> countries = new Dictionary<String, Country>()
        {
            {"SE", new Country()
            {
                Name = "Sweden",
                Code = "SE",
                Data = [new Data() { Name = "Water", Unit = "L", Points = [new DataPoint() { Date = new DateOnly(2023, 1, 1), Value = 100 }] }]
            }
            },
            {"NO", new Country()
            {
                Name = "Norway",
                Code = "NO",
                Data = [new Data() { Name = "Water", Unit = "L", Points = [new DataPoint() { Date = new DateOnly(2023, 1, 1), Value = 200 }] }]
            }
            },
            {"DK", new Country()
            {
                Name = "Denmark",
                Code = "DK",
                Data = [new Data() { Name = "Water", Unit = "L", Points = [new DataPoint() { Date = new DateOnly(2023, 1, 1), Value = 300 }] }]
            }
            }
        };

        DateOnly dateOnlyLol = new DateOnly(2023, 1, 1);
        // Dummy labels
        private readonly string[] dataLabels = { "Water", "Electricity", "CO2" };

        protected override async Task OnInitializedAsync()
        {
            //otherCountry = await apiHandler.FetchCountryOfTheDay(httpClient);
            //homeCountry = await apiHandler.FetchCountry("swe", httpClient);
        }

        private async Task UpdateHomeCountry(string code)
        {
            //homeCountry = await apiHandler.FetchCountry(selectedHome.ToLower(), httpClient);
            homeCountry = countries[code];
            StateHasChanged();
        }

        private async Task UpdateOtherCountry(string code)
        {
            //homeCountry = await apiHandler.FetchCountry(selectedHome.ToLower(), httpClient);
            otherCountry = countries[code];
            StateHasChanged();
        }
    }
}
    