using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;
using API;
using Client.API;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen.Blazor;


namespace Client.Components
{
    public partial class Custom
    {
        private List<string> availableCountries = new List<string>() { "swe", "nor", "fin", "den" };

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
        DateOnly dateOnlyLol = new DateOnly(2023, 1, 1);
        // Dummy labels
        private readonly string[] dataLabels = { "Water", "Electricity", "CO2" };

        protected override async Task OnInitializedAsync()
        {
            otherCountry = await apiHandler.FetchCountryOfTheDay(httpClient);
            homeCountry = await apiHandler.FetchCountry("swe", httpClient);
        }
    }
}
    