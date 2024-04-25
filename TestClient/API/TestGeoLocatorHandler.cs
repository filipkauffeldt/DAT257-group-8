using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.API;
using Moq;
using Moq.Protected;

namespace TestClient.API
{
    public class TestGeoLocatorHandler
    {
        private readonly IGeoLocator _geoLocator = new GeoLocatorHandler();
        private readonly string _apiUrl = "https://ipapi.co";

        [Fact]
        public async Task GetHomeISOAsync_CorrectEndpoint()
        {
            var url = $"{_apiUrl}/country_code_iso3/";

            var mockMsgHandler = new Mock<HttpMessageHandler>();
            mockMsgHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(r => r.Method == HttpMethod.Get && r.RequestUri == new Uri(url)),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new StringContent("ISO")
                });

            var mockClient = new HttpClient(mockMsgHandler.Object);
            var mockResponse = await _geoLocator.GetUserISOAsync(mockClient);

            mockMsgHandler.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get && req.RequestUri == new Uri(url)),
                ItExpr.IsAny<CancellationToken>());
        }
    }
}
