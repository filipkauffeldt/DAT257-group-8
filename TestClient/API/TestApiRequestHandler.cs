using API;
using Client.API;
using System.Net.Http.Json;
using Moq;
using Moq.Protected;

namespace TestClient.API
{
    public class TestApiRequestHandler
    {
        private readonly ApiRequestHandler _apiHandler = new ApiRequestHandler(new GeoLocatorHandler());
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _url = "http://localhost:5016";

        [Fact]
        public async Task TestFetchCountryOfTheDayCorrectType()
        {
            Country country = await _apiHandler.FetchCountryOfTheDay(_httpClient);
            Assert.True(country != null && country.GetType() == typeof(Country));
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
            Assert.True(apiHandlerCountry.Equals(rawFetchCountry));
        }

        [Fact]
        public async Task TestFetchCountryDataByYearCorrectEndpoint()
        {
            var code = "SWE";
            var date = new DateOnly(2020, 1, 1);
            var mockMsgHandler = new Mock<HttpMessageHandler>();
            mockMsgHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(r => r.Method == HttpMethod.Get && r.RequestUri == new Uri($"{_url}/Country/GetCountryDataForYear/{code}/{date.Year}-{date.Month}-{date.Day}")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(new Country
                    {
                        Code = "CODE",
                        Name = "Country Name"
                    }))
                });

            var mockClient = new HttpClient(mockMsgHandler.Object);
            var mockResponse = await _apiHandler.FetchCountryByYear(mockClient, code, date);

            mockMsgHandler.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get && req.RequestUri == new Uri($"{_url}/Country/GetCountryDataForYear/{code}/{date.Year}-{date.Month}-{date.Day}")),
                ItExpr.IsAny<CancellationToken>());
            Assert.Equal(new Country { Code = "CODE", Name = "Country Name" }, mockResponse);
        }
    }
}
