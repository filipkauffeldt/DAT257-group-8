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
        
        //ComparisonComponent LoadTestComp()
        //{
        //    return new ComparisonComponent()
        //    {
        //        ComparedCountry = GenerateRandomCountryTyp("Sweden", 100f),
        //        ComparedCountries = new List<Country> { GenerateRandomCountryTyp("BOlibompa", 200f) },
        //        ResourceType = "Water",
        //        Date = new DateOnly(2022, 1, 1)
        //    };
        //}

        //ComparisonComponent MockDataNearZeroValue()
        //{
        //    return new ComparisonComponent()
        //    {
        //        ComparedCountry = GenerateRandomCountryTyp("Sweden", 0.00001f),
        //        ComparedCountries = new List<Country> { GenerateRandomCountryTyp("BOlibompa", 200f) },
        //        ResourceType = "Water",
        //        Date = new DateOnly(2022, 1, 1),
        //        Unit = "liters"
        //    };
        //}

        [Fact]
        public void TestSpeciallyComparedText()
        {
            var comparedCountry = GenerateRandomCountryTyp("Sweden", 100f);
            var diffCountry = GenerateRandomCountryTyp("BOlibompa", 200f);
            ComparisonComponent comp = LoadTestComp();
            Assert.Equal("Sweden use the same amount of Water as BOlibompa", comp.SingleComparedText(comparedCountry, diffCountry, 100f, 100f, "Water"));
            Assert.Equal("Sweden use 100% more Water than BOlibompa", comp.SingleComparedText(comparedCountry, diffCountry, 200f, 100f, "Water"));
            Assert.Equal("Sweden use 50% less Water than BOlibompa", comp.SingleComparedText(comparedCountry, diffCountry, 50f, 100f, "Water"));

        }

        [Fact]
        public void TestGetComparisonPercentageNearZero()
        {
            var comparedCountry = GenerateRandomCountryTyp("Sweden", 100f);
            var diffCountry = GenerateRandomCountryTyp("BOlibompa", 200f);
            ComparisonComponent comp = MockDataNearZeroValue();
            Assert.Equal("Sweden use 200.00 liters more than BOlibompa", comp.SingleComparedText(comparedCountry, diffCountry, 200f, 0.000001f, "Water"));
            //Assert.Equal("Sweden use 200.00 liters less than BOlibompa", comp.SpeciallyComparedText(comparedCountry, diffCountry, 0.000001f, 200f, "Water"));
            //Assert.Equal("Sweden use the same amount of Water as BOlibompa", comp.SpeciallyComparedText(comparedCountry, diffCountry, 0, 0, "Water"));
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
            Assert.NotEqual(comp.ComparedCountries[0], comp.ComparedCountry);
        }
    }
}
