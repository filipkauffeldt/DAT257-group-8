using Microsoft.AspNetCore.Components;

using API;
using System.Linq;

namespace Client.Components
{
    public partial class ComparisonComponent
    {
        [Parameter]
        public required Country CountryOrigin { get; set; } 
        [Parameter]
        public required Country CountryComparison{get; set;}
        [Parameter]
        public string ResourceType { get; set; } = "NaN";

        [Parameter]
        public DateOnly date {  get; set; }

        public float OriginCountryValue { get; set; } = 1.0f;
        public float ComparisonCountryValue { get; set; } = 3.0f;
        public string Unit { get; set; } = "NaN";

        public string ComparisonValueStyle = "width: 10rem;";
        
        private Data? GetCountryData(Country country)
        {
            if (country != null)
            {
                var countryData = country.Data;
                if (countryData != null)
                {
                    return countryData.Where(d => d.Name == ResourceType).First();
                }
            }
            return null;
        }
        
        private string SetComparisonValues()
        {
                var resource1 = GetCountryData(CountryOrigin);

                var resource2 = GetCountryData(CountryComparison);

                if (resource1 != null && resource2 != null)
                {
                    OriginCountryValue = (float)resource1.Points.Where(dp => dp.Date.Year == date.Year).FirstOrDefault().Value;
                    ComparisonCountryValue = (float)resource2.Points.Where(dp => dp.Date.Year == date.Year).FirstOrDefault().Value;
                    Unit = resource1.Unit;

                }
                else
                {
                    return "No data available";
                }

            return GetComparisonPercentage(ComparisonCountryValue, OriginCountryValue);
        }

        private string GetComparisonPercentage(float comparisonValue, float originValue)
        {
            float relativeDifference = (comparisonValue / originValue) - 1;
            // Sets width of comparison value bar
            ComparisonValueStyle = ("width:" + (relativeDifference + 1) * 10 + "rem;").Replace(',', '.');


            if (ComparisonCountryValue > OriginCountryValue)
            {
                return (Math.Abs(relativeDifference) * 100).ToString("n2") + "% more";
            }
            else if (ComparisonCountryValue < OriginCountryValue)
            {
                return (Math.Abs(relativeDifference) * 100).ToString("n2") + "% less";
            }
            else
            {
                return "the same amount of";
            }
        }
    }
}
