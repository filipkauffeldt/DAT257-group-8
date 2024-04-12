using API;
using Client.API;
using System.Net.Http.Json;

namespace TestClient
{
    public class TestApiRequestHandler
    {
        private readonly ApiRequestHandler _apiHandler = new ApiRequestHandler();
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _url = "https://localhost:7262";

        [Fact]
        public async Task TestFetchCountryOfTheDayCorrectType()
        {
            Country country = await _apiHandler.FetchCountryOfTheDay(_httpClient);
            Assert.True(country != null && country.GetType() ==  typeof(Country));
        }

        [Fact]
        public async Task TestFetchCountryOfTheDayCorrectEndpoint()
        {
            Country apiHandlerCountry = await _apiHandler.FetchCountryOfTheDay(_httpClient);
            Country rawFetchCountry = await _httpClient.GetFromJsonAsync<Country>($"{_url}/Country/GetCountryOfTheDay");
            Assert.Equal(apiHandlerCountry, rawFetchCountry);
        }

        [Fact]
        public async Task TestFetchCountryCorrectType()
        {
            Country country = await _apiHandler.FetchCountry("swe", _httpClient);
            Assert.True(country != null && country.GetType() == typeof(Country));
        }

        [Fact]
        public async Task TestFetchCountryCorrectEndpoint()
        {
            string iso = "swe";
            Country apiHandlerCountry = await _apiHandler.FetchCountry(iso, _httpClient);
            Country rawFetchCountry = await _httpClient.GetFromJsonAsync<Country>($"{_url}/Country/GetCountry/{iso}");
            Assert.Equal(apiHandlerCountry, rawFetchCountry);
        }

        [Fact]
        public async Task TestFetchAllCountriesCorrectType()
        {
            List<Country> countries = await _apiHandler.FetchAllCountries(_httpClient);
            Assert.True(countries != null && countries.GetType() == typeof(List<Country>));
        }

        [Fact]
        public async Task TestFetchAllCountriesCorrectEndpoint()
        {
            List<Country> apiHandlerCountry = await _apiHandler.FetchAllCountries(_httpClient);
            List<Country> rawFetchCountry = await _httpClient.GetFromJsonAsync<List<Country>>($"{_url}/Country/GetAllCountries/");
            Assert.Equal(apiHandlerCountry, rawFetchCountry);
        }
    }
}