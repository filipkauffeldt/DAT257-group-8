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

        public async Task<Country> FetchCountry(string iso, HttpClient httpClient)
        {
            try
            {
                return await httpClient.GetFromJsonAsync<Country>($"{apiUrl}/Country/GetCountry/{iso}");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception($"Api call failed, {e}");
            }
        }

        public async Task<IEnumerable<Country>> FetchAllCountries(HttpClient httpClient)
        {
            try
            {
                return await httpClient.GetFromJsonAsync<IEnumerable<Country>>($"{apiUrl}/Country/GetAllCountries");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception($"Api call failed, {e}");
            }
        }
    }
}
