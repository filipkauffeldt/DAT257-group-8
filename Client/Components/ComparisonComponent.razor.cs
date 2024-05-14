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
        public required Country CountryComparison { get; set; }

        [Parameter]
        public required IList<Country> CountryList { get; set;}

        [Parameter]
        public string ResourceType { get; set; } = "NaN";

        [Parameter]
        public required DateOnly date {  get; set; }

        public float OriginCountryValue { get; set; } = 1.0f;
        public List<DataPoint> OriginValueList = new List<DataPoint>();//  = [new DataPoint() { Date = new DateOnly(2023, 1, 1), Value = 1.0 }];
        
        public float ComparisonCountryValue { get; set; } = 3.0f;
        public List<DataPoint> ComparisonValueList = new List<DataPoint>(); //= [new DataPoint() { Date = new DateOnly(2023, 1, 1), Value = 2.0 }];
        private IDictionary<string, IList<DataPoint>> ValueMap = new Dictionary<string, IList<DataPoint>>();

        public string Unit { get; set; } = "NaN";

        public string ComparisonValueStyle = "width: 10rem;";

        private float Threshold = 0.0001f;

        public string ConsumptionText = "Consumption";

        private Data? GetCountryData(Country country)
        {
            
            var countryData = country.Data;
            if (countryData != null)
            {
                    return countryData.Where(d => d.Name == ResourceType).FirstOrDefault();
            }
            return null;
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            LoadValues();
        }

        // Update values on parameter change
        protected override void OnParametersSet()
        {
            LoadValues();
            StateHasChanged();
        }

        public void LoadValues()
        {
            foreach (var country in CountryList)
            {
                var resource = GetCountryData(country);
                ValueMap.Remove(country.Name);
                if (resource != null)
                {
                    var valueList = new List<DataPoint> {
                        new DataPoint { Date = date, Value = resource.Points.Where(dp => dp.Date.Year == date.Year).FirstOrDefault()?.Value ?? 1 }
                    };
                    ValueMap.Add(country.Name, valueList);
                    Unit = resource.Unit;
                }
                else
                {
                    var valueList = new List<DataPoint> {
                        new DataPoint { Date = date, Value = 1 }
                    };
                    ValueMap.Add(country.Name, valueList);
                }
            }
            
            // Text at the bottom of the cards
            ConsumptionText = "Consumption in " + Unit;
        }

        public string GetComparisonPercentage(float comparisonValue, float originValue)
        {  // Threshold makes ut so that values near 0 do not cause a extremely large percentage value in the comparison text.
            if( (Math.Abs(comparisonValue) < this.Threshold) && (Math.Abs(originValue) < Threshold))
            {
                return "the same amount of";
            } else if (Math.Abs(comparisonValue) < Threshold)
            {
                return (originValue.ToString() + " " + Unit + " less ");
            }
            else if((Math.Abs(originValue) < Threshold))
            {
                return (comparisonValue.ToString() + " " + Unit + " more ");
            }
            float relativeDifference = (comparisonValue / originValue) - 1;
            
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
        // Sets width of comparison value bar
        public float SetBarWidth()
        {
            float maxValue = ValueMap.Values.Max(valueList => (float)valueList.Max(dataPoint => dataPoint.Value));
            return maxValue * 1.3f;
        }
    }
}
