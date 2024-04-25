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
        private readonly Country _mockCountry = new() { Code = "CODE", Name = "Country Name" };

        [Fact]
        public async Task TestFetchCountryOfTheDayCorrectType()
        {
            Country country = await _apiHandler.FetchCountryOfTheDay(_httpClient);
            Assert.True(country != null && country.GetType() == typeof(Country));
        }

        [Fact]
        public async Task TestFetchCountryOfTheDayCorrectEndpoint()
        {
            var url = $"{_url}/Country/GetCountryOfTheDay";
            var mockMsgHandler = new Mock<HttpMessageHandler>();
            mockMsgHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(r => r.Method == HttpMethod.Get && r.RequestUri == new Uri(url)),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(_mockCountry))
                });

            var mockClient = new HttpClient(mockMsgHandler.Object);
            var mockResponse = await _apiHandler.FetchCountryOfTheDay(mockClient);

            mockMsgHandler.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get && req.RequestUri == new Uri(url)),
                ItExpr.IsAny<CancellationToken>());
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
            var code = "CODE";
            var url = $"{_url}/Country/GetCountry/{code}";

            var mockMsgHandler = new Mock<HttpMessageHandler>();
            mockMsgHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(r => r.Method == HttpMethod.Get && r.RequestUri == new Uri(url)),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(_mockCountry))
                });

            var mockClient = new HttpClient(mockMsgHandler.Object);
            var mockResponse = await _apiHandler.FetchCountry(code, mockClient);

            mockMsgHandler.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get && req.RequestUri == new Uri(url)),
                ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task TestFetchCountryDataByYearCorrectEndpoint()
        {
            var code = "CODE";
            var date = new DateOnly(2020, 1, 1);
            var url = $"{_url}/Country/GetCountryDataForYear/{code}/{date.Year}-{date.Month}-{date.Day}";

            var mockMsgHandler = new Mock<HttpMessageHandler>();
            mockMsgHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(r => r.Method == HttpMethod.Get && r.RequestUri == new Uri(url)),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(_mockCountry))
                });

            var mockClient = new HttpClient(mockMsgHandler.Object);
            var mockResponse = await _apiHandler.FetchCountryByYear(mockClient, code, date);

            mockMsgHandler.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get && req.RequestUri == new Uri(url)),
                ItExpr.IsAny<CancellationToken>());
        }
    }
}
