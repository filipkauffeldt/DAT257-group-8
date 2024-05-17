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
        public void TestSingleComparedText()
        {
            var originCountry = GenerateCountry("Sweden", 100f);
            var diffCountry = GenerateCountry("BOlibompa", 200f);
            ComparisonComponent comp = LoadTestComp();
            Assert.Equal("Sweden use the same amount of Water as BOlibompa", comp.SingleComparedText(originCountry, diffCountry, 100f, 100f, "Water", "liter(s)"));
            Assert.Equal("Sweden use 100% more Water than BOlibompa", comp.SingleComparedText(originCountry, diffCountry, 200f, 100f, "Water", "liter"));
            Assert.Equal("Sweden use 50% less Water than BOlibompa", comp.SingleComparedText(originCountry, diffCountry, 50f, 100f, "Water", "liter"));

        }

        [Fact]
        public void TestSingleComparedTextNearZero()
        {
            var originCountry = GenerateCountry("Sweden", 100f);
            var diffCountry = GenerateCountry("BOlibompa", 200f);
            ComparisonComponent comp = MockDataNearZeroValue();
            Assert.Equal("Sweden use 200 liter more Water than BOlibompa", comp.SingleComparedText(originCountry, diffCountry, 200f, 0.000001f, "Water", "liter"));
            Assert.Equal("Sweden use 200 liter less Water than BOlibompa", comp.SingleComparedText(originCountry, diffCountry, 0.000001f, 200f, "Water", "liter"));
            Assert.Equal("Sweden use the same amount of Water as BOlibompa", comp.SingleComparedText(originCountry, diffCountry, 0.000001f, 0, "Water", "liter"));
        }

        [Fact]
        public void TestMultipleComparedTextEqualValue()
        {
            var originCountry = GenerateCountry("Sweden", 100f);
            var comparedCountry = GenerateCountry("BOlibompa", 200f);
            var equalValueMap = new Dictionary<Country, double>
            {
                { originCountry, 100f },
                { comparedCountry, 100f }
            };
            ComparisonComponent comp = LoadTestComp();
            Assert.Equal(
                "Sweden uses the same amount of Water as the average",
                comp.MultipleComparedText(originCountry, equalValueMap, "Water", "liter")
            );
        }

        [Fact]
        public void TestMultipleComparedTextLowerThanAverage()
        {
            var originCountry = GenerateCountry("Sweden", 100f);
            var comparedCountry = GenerateCountry("BOlibompa", 200f);
            var lowerValueMap = new Dictionary<Country, double>
            {
                { originCountry, 40f },
                { comparedCountry, 100f }
            };
            ComparisonComponent comp = LoadTestComp();
            Assert.Equal(
                "Sweden uses 60% less Water than the average",
                comp.MultipleComparedText(originCountry, lowerValueMap, "Water", "liter")
            );
        }

        [Fact]
        public void TestMultipleComparedTextHigherThanAverage()
        {
            var originCountry = GenerateCountry("Sweden", 100f);
            var comparedCountry = GenerateCountry("BOlibompa", 200f);
            var higherValueMap = new Dictionary<Country, double>
            {
                { originCountry, 200f },
                { comparedCountry, 100f }
            };
            ComparisonComponent comp = LoadTestComp();
            Assert.Equal(
                "Sweden uses 100% more Water than the average",
                comp.MultipleComparedText(originCountry, higherValueMap, "Water", "liter")
            );
        }

        [Fact]
        public void TestMultipleComparedTextLessThanAverageNearZero()
        {
            var originCountry = GenerateCountry("Sweden", 0.000001f);
            var comparedCountry = GenerateCountry("BOlibompa", 200f);
            var nearZeroValueMap = new Dictionary<Country, double>
            {
                { originCountry, 0.000001f },
                { comparedCountry, 100f }
            };
            ComparisonComponent comp = MockDataNearZeroValue();
            Assert.Equal(
                "Sweden uses 100 liter less Water than the average",
                comp.MultipleComparedText(originCountry, nearZeroValueMap, "Water", "liter")
            );
        }

        [Fact]
        public void TestMultipleComparedTextHigherThanAverageNearZero()
        {
            var originCountry = GenerateCountry("Sweden", 200f);
            var comparedCountry = GenerateCountry("BOlibompa", 0.000001f);
            var nearZeroValueMap = new Dictionary<Country, double>
            {
                { originCountry, 200f },
                { comparedCountry, 0.000001f }
            };
            ComparisonComponent comp = MockDataNearZeroValue();
            Assert.Equal(
                "Sweden uses 200 liter more Water than the average",
                comp.MultipleComparedText(originCountry, nearZeroValueMap, "Water", "liter")
            );
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
