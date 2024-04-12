using API.Contracts;
using Client.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestClient.Components
{
    public class ComparisonComponentTest
    {
        // Used for simple test, XUnit does not exist yet in the project
        public static Country GenerateRandomCountryTyp(String countryName,float dataValue)
        {
            DataPoint point = new DataPoint
            {
                DateTime = "2023",
                Value = dataValue,
            };

            Data data = new Data()
            {
                Name = "Water",
                Unit = "L",
                Points = [point]
            };

            Country country = new Country()
            {
                Code = "Lall",
                Name = countryName,
                Description = "Bestest country",
                Data = [data]
            };

            return country;
        }
        ComparisonComponent comp = new ComparisonComponent() {
            CountryComparison = GenerateRandomCountryTyp("Sweden",100f),
            CountryOrigin = GenerateRandomCountryTyp("BOlibompa",200f),
            ResourceType = "Water"
        };

        [Fact]
        public void TestGetComparisonPercentage()
        {
            Assert.True(comp.);
        }
    }
}
