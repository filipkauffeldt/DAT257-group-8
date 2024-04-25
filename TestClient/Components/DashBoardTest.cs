using API;
using Client.API;
using Client.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestClient.Components
{
    public class DashBoardTest
    {
        DashBoard dashBoard = new DashBoard();
        HomeCountryDropDown dropDown = new HomeCountryDropDown();
        private readonly ApiRequestHandler _apiHandler = new ApiRequestHandler();
        private readonly HttpClient _httpClient = new HttpClient();
        /*[Fact]
        public void TestHomeCountryChange()
        {
            var dashboard = new DashBoard();
            dashBoard.HomeCountryChange("SWE");
            Country originalCountry = dashBoard.countryCompTwo;
            //Console.WriteLine(originalCountry.Name);
            //dashBoard.HomeCountryChange("DNK");
            //dashBoard.HomeCountryChange("SWE");
            Assert.Equal(originalCountry, dashBoard.countryCompTwo);
        }
        */
    }
}
