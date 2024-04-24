using API;
using Client.API;
using System.Net.Http.Json;

namespace TestClient.API
{
    public class TestApiRequestHandler
    {
        private readonly ApiRequestHandler _apiHandler = new ApiRequestHandler(new GeoLocatorHandler());
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
            Country country = await _apiHandler.FetchCountry("SWE", _httpClient);
            Assert.True(country != null && country.GetType() == typeof(Country));
        }

        [Fact]
        public async Task TestFetchCountryCorrectEndpoint()
        {
            string iso = "SWE";
            Country apiHandlerCountry = await _apiHandler.FetchCountry(iso, _httpClient);
            Country rawFetchCountry = await _httpClient.GetFromJsonAsync<Country>($"{_url}/Country/GetCountry/{iso}");
            //Assert.Equal(apiHandlerCountry, rawFetchCountry);
            Assert.True(apiHandlerCountry.Equals(rawFetchCountry));
        }

        [Fact]
        public async Task FetchCountryDataByYearCorrectEndpoint()
        {
            var year = new DateOnly(2020, 1, 1);
            string code = "SWE";

            var apiHandlerData = await _apiHandler.FetchCountryByYear(_httpClient, code, year);
            var rawFetchData = await _httpClient.GetFromJsonAsync<Country>($"{_url}/Country/GetCountryDataForYear/{code}/{year}");

            var dataDiff = apiHandlerData.Data?.Intersect(rawFetchData.Data);
            Assert.True(dataDiff != null && !dataDiff.Any());
        }
    }
}