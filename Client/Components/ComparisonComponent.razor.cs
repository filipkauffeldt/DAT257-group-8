using Microsoft.AspNetCore.Components;

using API;
using System.Linq;
using Radzen.Blazor;

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
        public required DateOnly date {  get; set; }

        public float OriginCountryValue { get; set; } = 1.0f;
        public List<DataPoint> OriginValueList = new List<DataPoint>();//  = [new DataPoint() { Date = new DateOnly(2023, 1, 1), Value = 1.0 }];
        
        public float ComparisonCountryValue { get; set; } = 3.0f;
        public List<DataPoint> ComparisonValueList = new List<DataPoint>(); //= [new DataPoint() { Date = new DateOnly(2023, 1, 1), Value = 2.0 }];

        public string Unit { get; set; } = "NaN";

        public string ComparisonValueStyle = "width: 10rem;";
        
        private Data? GetCountryData(Country country)
        {
            
            var countryData = country.Data;
            if (countryData != null)
            {
                return countryData.Where(d => d.Name == ResourceType).First();
            }
            
            return null;
        }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            LoadValues();

        }
        private void LoadValues()
        {
            var resource1 = GetCountryData(CountryOrigin);  
            var resource2 = GetCountryData(CountryComparison);
            if (resource1 != null && resource2 != null)
            {
                OriginValueList.Clear();
                ComparisonValueList.Clear();
                OriginValueList.Add(resource1.Points.Where(dp => dp.Date.Year == date.Year).First());
                ComparisonValueList.Add(resource2.Points.Where(dp => dp.Date.Year == date.Year).First());
                OriginCountryValue = (float)OriginValueList[0].Value;
                this.ComparisonCountryValue = (float)ComparisonValueList[0].Value;
                Unit = resource1.Unit;
            }
        }

        public string GetComparisonPercentage(float comparisonValue, float originValue)
        {
            float relativeDifference = (comparisonValue / originValue) - 1;
            // Sets width of comparison value bar
            if (comparisonValue > originValue)
            {
                return ((int)(Math.Abs(relativeDifference) * 100)).ToString() + "% more";
            }
            else if (comparisonValue < originValue)
            {
                return ((int)(Math.Abs(relativeDifference) * 100)).ToString() + "% less";
            }
            else
            {
                return "the same amount of";
            }
        }

        public float setBarWidth()
        {
            return MathF.Max(ComparisonCountryValue, OriginCountryValue) * 1.1f;
        }
    }
}
