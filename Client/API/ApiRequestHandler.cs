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

        public async Task<Country> FetchCountryOfTheDayAsync(HttpClient httpClient)
        {
            return await new RequestWrapper<Country>().GetFromJSONAsync(httpClient, $"{apiUrl}/Country/GetCountryOfTheDay");
        }

        public async Task<Country> FetchCountryAsync(string iso, HttpClient httpClient)
        {
            return await new RequestWrapper<Country>().GetFromJSONAsync(httpClient, $"{apiUrl}/Country/GetCountry/{iso}");
        }

        public async Task<IEnumerable<Country>> FetchAllCountriesAsync(HttpClient httpClient)
        {
            return await new RequestWrapper<IEnumerable<Country>>().GetFromJSONAsync(httpClient, $"{apiUrl}/Country/GetAllCountries");
        }

        public async Task<IEnumerable<Country>> FetchAllCountryIdentifiers(HttpClient httpClient)
        {
            return await new RequestWrapper<IEnumerable<Country>>().GetFromJSONAsync(httpClient, $"{apiUrl}/Country/GetAllCountryIdentifiers");
        }

        public async Task<Country> FetchCountryByYearAsync(HttpClient httpClient, string code, DateOnly date)
        {
            return await new RequestWrapper<Country>().GetFromJSONAsync(httpClient, $"{apiUrl}/Country/GetCountryDataForYear/{code}/{date.Year}-{date.Month}-{date.Day}");
        }

        public async Task<Country> FetchHomeCountryAsync(HttpClient httpClient)
        {
            var iso = await _geoLocator.GetUserISOAsync(httpClient);
            return await FetchCountryAsync(iso, httpClient);
        }
    }
}