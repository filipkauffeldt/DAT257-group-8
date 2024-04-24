using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.API;

namespace TestClient.API
{
    public class TestGeoLocatorHandler
    {
        private readonly IGeoLocator geoLocator = new GeoLocatorHandler();
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiUrl = "https://ipapi.co";
        private string? _clientIp;
        private string ClientIP
        {
            get
            {
                _clientIp ??= ApiGlobal.GetClientIPAsync(httpClient).Result;
                return _clientIp;
            }
        }


        [Fact]
        public async Task GetHomeISOAsync_ReturnsHomeISO()
        {
            // Act
            var result = await geoLocator.GetHomeISOAsync(httpClient);

            // Assert
            Assert.Equal("SWE", result);
        }

        [Fact]
        public async Task GetHomeISOAsync_CorrectEndpoint()
        {
            // Act
            var result = await geoLocator.GetHomeISOAsync(httpClient);
            var rawResult = await httpClient.GetStringAsync($"{apiUrl}/{ClientIP}/country_code_iso3/");

            // Assert
            Assert.Equal(rawResult, result);
        }
    }
}
