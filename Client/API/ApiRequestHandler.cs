using API;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;


namespace Client.API
{
    public static class ApiRequestHandler
    {
        private static string apiUrl = "https://localhost:7262";

        private static readonly HttpClient _httpClient = new();
        public static async Task<Country> FetchCountryOfTheDay()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Country>($"{apiUrl}/Country/GetCountryOfTheDay");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception($"Api call failed, {e}");
            }
        }

        public static async Task<Country> FetchCountry(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Country>($"{apiUrl}/Country/GetCountry/{id}");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception($"Api call failed, {e}");
            }
        }

        public static async Task<List<Country>> FetchAllCountries()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Country>>($"{apiUrl}/Country/GetAllCountries");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception($"Api call failed, {e}");
            }
        }
    }
}
