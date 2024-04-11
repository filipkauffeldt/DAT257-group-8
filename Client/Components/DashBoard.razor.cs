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
        private readonly string[] dataLabels = { "Water", "Electricity", "CO2" };

        protected override async Task OnInitializedAsync()
        {
            countryOfTheDay = await ApiRequestHandler.FetchCountryOfTheDay();
            var homeCountry = await ApiRequestHandler.FetchCountry(1);
            comparedCountries.Add(homeCountry);
        }
    }
}
