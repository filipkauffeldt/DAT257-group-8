using API;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;


namespace Client.API
{
    public class ApiRequestHandler : IApiHandler
    {
        private string apiUrl = "http://localhost:5016";
        private IGeoLocator _geoLocator;

        public ApiRequestHandler(IGeoLocator geoLocator)
        {
            _geoLocator = geoLocator;
        }

        public async Task<Country> FetchCountryOfTheDay(HttpClient httpClient)
        {
            try
            {
                var response = await httpClient.GetAsync($"{apiUrl}/Country/GetCountryOfTheDay");
                var country = await response.Content.ReadFromJsonAsync<Country>();
                if (country == null)
                {
                    throw new Exception("GetCountryOfTheDay returned null");
                }
                return country;

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
                var response = await httpClient.GetAsync($"{apiUrl}/Country/GetCountry/{iso}");
                var country = await response.Content.ReadFromJsonAsync<Country>();
                if (country == null)
                {
                    throw new Exception("GetCountry returned null");
                }
                return country;

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
                var response = await httpClient.GetAsync($"{apiUrl}/Country/GetAllCountries");
                var countries = await response.Content.ReadFromJsonAsync<IEnumerable<Country>>();
                if (countries == null)
                {
                    throw new Exception("GetAllCountries returned null");
                }
                return countries;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception($"Api call failed, {e}");
            }
        }

        public async Task<IEnumerable<Country>> FetchAllCountryIdentifiers(HttpClient httpClient)
        {
            try
            {
                var response = await httpClient.GetAsync($"{apiUrl}/Country/GetAllCountryIdentifiers");
                var countryIdentifiers = await response.Content.ReadFromJsonAsync<IEnumerable<Country>>();
                if (countryIdentifiers == null)
                {
                    throw new Exception("GetAllCountryIdentifiers returned null");
                }
                return countryIdentifiers;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception($"Api call failed, {e}");
            }
        }

        public async Task<Country> FetchCountryByYear(HttpClient httpClient, string code, DateOnly date)
        {
            try
            {
                var response = await httpClient.GetAsync($"{apiUrl}/Country/GetCountryDataForYear/{code}/{date.Year}-{date.Month}-{date.Day}");
                var country = await response.Content.ReadFromJsonAsync<Country>();
                if (country == null)
                {
                    throw new Exception("GetCountryDataForYear returned null");
                }
                return country;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception($"Api call failed, {e}");
            }
        }

        public async Task<Country> FetchHomeCountry(HttpClient httpClient)
        {
            var iso = await _geoLocator.GetUserISOAsync(httpClient);
            return await FetchCountry(iso, httpClient);
        }
    }
}