using Microsoft.AspNetCore.Components;

using API;
using System.Linq;
using Radzen.Blazor;
using System.Drawing;

namespace Client.Components
{
    public partial class ComparisonComponent
    {
        [Parameter]
        public Country? ComparedCountry { get; set; }
        [Parameter]
        public required Country CountryComparison { get; set; }

        [Parameter]
        public required IList<Country> CountryList { get; set;}

        [Parameter]
        public string ResourceType { get; set; } = "NaN";

        [Parameter]
        public required DateOnly date {  get; set; }

        private IDictionary<string, IList<DataPoint>> ValueMap = new Dictionary<string, IList<DataPoint>>();
        // "#1E3D58"
        // "#E14177"
        private IList<string> _colors = new List<string>
        {
            "#1E3D58", // Them Dark Blue
            "#E14177", // Them Red/Pink
            "#006699", // Blue
            "#FF9900", // Orange
            "#3399CC", // Brighter Blue
            "#FFCC00", // Yellow
            "#336699", // Blue
            "#FF6600", // Orange
            "#003399", // Blue
            "#993300", // Red
            "#003366", // Dark Blue
            "#FF0066", // Pink
            "#006699", // Blue
            "#FF9933", // Orange
            "#336699", // Blue
            "#FF6633", // Orange
            "#003366", // Dark Blue
            "#FF3300", // Red
            "#009999", // Blue
            "#FF6600"  // Orange
        };

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
            var countries = new List<Country>();
            if (ComparedCountry != null) countries.Add(ComparedCountry);
            countries.AddRange(CountryList);

            foreach (var country in countries)
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
