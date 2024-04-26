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

        private Mock<HttpMessageHandler> CreateMockMsgHandler(string url) {
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
            return mockMsgHandler;
        }

        [Fact]
        public async Task TestFetchCountryOfTheDayCorrectEndpoint()
        {
            var url = $"{_url}/Country/GetCountryOfTheDay";
            var mockMsgHandler = CreateMockMsgHandler(url);
            var mockClient = new HttpClient(mockMsgHandler.Object);
            var mockResponse = await _apiHandler.FetchCountryOfTheDayAsync(mockClient);

            mockMsgHandler.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get && req.RequestUri == new Uri(url)),
                ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task TestFetchCountryCorrectEndpoint()
        {
            var code = "CODE";
            var url = $"{_url}/Country/GetCountry/{code}";
            var mockMsgHandler = CreateMockMsgHandler(url);
            var mockClient = new HttpClient(mockMsgHandler.Object);
            var mockResponse = await _apiHandler.FetchCountryAsync(code, mockClient);

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
            var mockMsgHandler = CreateMockMsgHandler(url);
            var mockClient = new HttpClient(mockMsgHandler.Object);
            var mockResponse = await _apiHandler.FetchCountryByYearAsync(mockClient, code, date);

            mockMsgHandler.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get && req.RequestUri == new Uri(url)),
                ItExpr.IsAny<CancellationToken>());
        }
    }
}
