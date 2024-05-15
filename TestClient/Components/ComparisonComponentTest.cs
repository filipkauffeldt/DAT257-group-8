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
   
        public static Country GenerateCountry(string countryName, float dataValue)
        {
            DataPoint point = new DataPoint
            {
                Date = new DateOnly(2022, 1, 1),
                Value = dataValue,
            };

            Data data = new Data()
            {
                Name = "Water",
                Unit = "Liters",
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

        ComparisonComponent LoadTestComp()
        {
            return new ComparisonComponent()
            {
                OriginCountry = GenerateCountry("Sweden", 100f),
                ComparedCountries = new List<Country> { GenerateCountry("BOlibompa", 200f) },
                ResourceType = "Water",
                Date = new DateOnly(2022, 1, 1)
            };
        }

        ComparisonComponent MockDataNearZeroValue()
        {
            return new ComparisonComponent()
            {
                OriginCountry = GenerateCountry("Sweden", 0.00001f),
                ComparedCountries = new List<Country> { GenerateCountry("BOlibompa", 200f) },
                ResourceType = "Water",
                Date = new DateOnly(2022, 1, 1),
                Unit = "liters"
            };
        }

        [Fact]
        public void TestSpeciallyComparedText()
        {
            var comparedCountry = GenerateCountry("Sweden", 100f);
            var diffCountry = GenerateCountry("BOlibompa", 200f);
            ComparisonComponent comp = LoadTestComp();
            Assert.Equal("Sweden use the same amount of Water as BOlibompa", comp.SingleComparedText(comparedCountry, diffCountry, 100f, 100f, "Water", "liter(s)"));
            Assert.Equal("Sweden use 100% more Water than BOlibompa", comp.SingleComparedText(comparedCountry, diffCountry, 200f, 100f, "Water", "liter"));
            Assert.Equal("Sweden use 50% less Water than BOlibompa", comp.SingleComparedText(comparedCountry, diffCountry, 50f, 100f, "Water", "liter"));

        }

        [Fact]
        public void TestGetComparisonPercentageNearZero()
        {
            var comparedCountry = GenerateCountry("Sweden", 100f);
            var diffCountry = GenerateCountry("BOlibompa", 200f);
            ComparisonComponent comp = MockDataNearZeroValue();
            Assert.Equal("Sweden use 200 liter more Water than BOlibompa", comp.SingleComparedText(comparedCountry, diffCountry, 200f, 0.000001f, "Water", "liter"));
            Assert.Equal("Sweden use 200 liter less Water than BOlibompa", comp.SingleComparedText(comparedCountry, diffCountry, 0.000001f, 200f, "Water", "liter"));
            Assert.Equal("Sweden use the same amount of Water as BOlibompa", comp.SingleComparedText(comparedCountry, diffCountry, 0.000001f, 0, "Water", "liter"));
        }

        [Fact]
        public void TestGetComparisonResourceType()
        {
            ComparisonComponent comp = LoadTestComp();
            Assert.True(comp.ResourceType != null && comp.ResourceType != "Nan");
        }

        [Fact]
        public void TestCountriesNotEqual()
        {
            ComparisonComponent comp = LoadTestComp();
            Assert.NotEqual(comp.ComparedCountries[0], comp.OriginCountry);
        }
    }
}
