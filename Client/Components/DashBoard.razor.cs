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
        
        // Dummy labels
        private readonly string[] dataLabels = { "Water", "Electricity", "CO2" };

        protected override async Task OnInitializedAsync()
        {
            countryOfTheDay = await apiHandler.FetchCountryOfTheDay(httpClient);
            var homeCountry = await apiHandler.FetchCountry("swe", httpClient);
            comparedCountries.Add(homeCountry);
        }
    }
}
