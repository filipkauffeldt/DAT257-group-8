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
            Data = [new Data() { Name = "Water", Unit = "L", Points = [new DataPoint() { Date = new DateOnly(2023,1,1), Value = 100}] }]
        };
        private Country countryCompTwo = new Country()
        {
            Name = "BOgusland",
            Code = "BO",
            Data = [new Data() { Name = "Water", Unit = "L", Points = [new DataPoint() { Date = new DateOnly(2023, 1, 1), Value = 200 }] }]
        };
        DateOnly dateOnlyLol = new DateOnly(2023, 1, 1);
        // Dummy labels
        private readonly string[] dataLabels = { "Water", "Electricity", "CO2" };

        protected override async Task OnInitializedAsync()
        {
            //countryOfTheDay = await apiHandler.FetchCountryOfTheDay(httpClient);
            //var homeCountry = await apiHandler.FetchCountry("swe", httpClient);
            //comparedCountries.Add(homeCountry);
        }
    }
}
