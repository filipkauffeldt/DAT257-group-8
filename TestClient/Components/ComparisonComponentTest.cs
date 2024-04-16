using API;
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
   
        public static Country GenerateRandomCountryTyp(String countryName,float dataValue)
        {
            DataPoint point = new DataPoint
            {
                Date = new DateOnly(2022, 1, 1),
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
            ResourceType = "Water",
            date = new DateOnly(2022,1,1)
        };

        [Fact]

        public void TestGetComparisonPercentage()
        {
            Assert.Equal("the same amount of", comp.GetComparisonPercentage(100f, 100f));
            Assert.Equal("100% more", comp.GetComparisonPercentage(200f, 100f));
            Assert.Equal("50% less", comp.GetComparisonPercentage(50f, 100f));
        }

        [Fact]
        public void TestGetComparisonResourceType()
        {
            Assert.True(comp.ResourceType != null && comp.ResourceType != "Nan");
        }

        [Fact]
        public void TestCountriesNotEqual()
        {
            Assert.NotEqual(comp.CountryOrigin, comp.CountryComparison);
        }
    }
}
