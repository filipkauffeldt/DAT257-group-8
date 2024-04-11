using System.Net.Http.Json;
using API.Contracts;
using Radzen.Blazor;


namespace Client.Components
{
    public partial class DashBoard
    {
        private Country? countryOfTheDay;
        private List<Country> data = new List<Country>(); 
        protected override async Task OnInitializedAsync()
        {
            try
            {
                countryOfTheDay = await httpClient.GetFromJsonAsync<Country>("https://localhost:7262/Country/GetCountryOfTheDay");
                Country baseCountry = await httpClient.GetFromJsonAsync<Country>("https://localhost:7262/Country/GetCountry?id=sweden");
                data.Add(baseCountry);
            }
            catch (Exception e) { 
                Console.WriteLine(e);
            }
        }
    }
}
