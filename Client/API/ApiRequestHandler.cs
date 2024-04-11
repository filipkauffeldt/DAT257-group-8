using API;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;


namespace Client.API
{
    public class ApiRequestHandler : IApiHandler
    {
        private string apiUrl = "https://localhost:7262";

        public async Task<Country> FetchCountryOfTheDay(HttpClient httpClient)
        {
            try
            {
                return await httpClient.GetFromJsonAsync<Country>($"{apiUrl}/Country/GetCountryOfTheDay");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception($"Api call failed, {e}");
            }
        }

        public async Task<Country> FetchCountry(int id, HttpClient httpClient)
        {
            try
            {
                return await httpClient.GetFromJsonAsync<Country>($"{apiUrl}/Country/GetCountry/{id}");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception($"Api call failed, {e}");
            }
        }

        public async Task<List<Country>> FetchAllCountries(HttpClient httpClient)
        {
            try
            {
                return await httpClient.GetFromJsonAsync<List<Country>>($"{apiUrl}/Country/GetAllCountries");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception($"Api call failed, {e}");
            }
        }
    }
}
