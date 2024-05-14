using Microsoft.AspNetCore.Components;

using API;

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
        public required DateOnly Date {  get; set; }

        private IDictionary<string, IList<DataPoint>> ValueMap = new Dictionary<string, IList<DataPoint>>();

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
                        new DataPoint { Date = Date, Value = resource.Points.Where(dp => dp.Date.Year == Date.Year).FirstOrDefault()?.Value ?? 1 }
                    };
                    ValueMap.Add(country.Name, valueList);
                    Unit = resource.Unit;
                }
                else
                {
                    var valueList = new List<DataPoint> {
                        new DataPoint { Date = Date, Value = 1 }
                    };
                    ValueMap.Add(country.Name, valueList);
                }
            }
            
            // Text at the bottom of the cards
            ConsumptionText = "Consumption in " + Unit;
        }

        public string GetComparisonText()
        {
            if (ComparedCountry != null)
            {
                return SpeciallyComparedText();
            }
            else
            {
                return GeneralComparedText();
            }
        }

        private string GeneralComparedText()
        {
            var maxUsageCountry = ValueMap.Aggregate((left, right) => left.Value[0].Value > right.Value[0].Value ? left : right).Key;
            var maxUseValue = ValueMap[maxUsageCountry][0].Value;

            var secondUsageCountry = ValueMap.OrderByDescending(kv => kv.Value[0].Value)
                                  .Skip(1)
                                  .FirstOrDefault()
                                  .Key;

            var secondUseValue = ValueMap[secondUsageCountry][0].Value;
            if (maxUseValue < Threshold || secondUseValue < Threshold)
            {
                return $"{maxUsageCountry} uses {maxUseValue-secondUseValue} {Unit} more {ResourceType} than the second most consuming country";
            }


            var percentage = Math.Round((maxUseValue / secondUseValue - 1) * 100);
            var comparisonText = $"{maxUsageCountry} use {percentage}% more {ResourceType} than the second most consuming country";
            return comparisonText;
        }

        private string SpeciallyComparedText()
        {
            float comparisonValue = (float)ValueMap[ComparedCountry!.Name][0].Value;
            var biggestDiffCountry = GetCountryWithBiggestDifference(comparisonValue);
            var diffValue = biggestDiffCountry?.Data?.Select(d => d.Points.Where(dp => dp.Date == Date).FirstOrDefault()?.Value).FirstOrDefault() ?? 0;


            string comparisonText = $"{ComparedCountry.Name} uses ";

            if ((Math.Abs(comparisonValue) < this.Threshold) && (Math.Abs(diffValue) < Threshold))
            {
                comparisonText += "the same amount of " + Unit;
            }
            else if (Math.Abs(comparisonValue) < Threshold)
            {
                comparisonText += (diffValue.ToString() + " " + Unit + " less ");
            }
            else if ((Math.Abs(diffValue) < Threshold))
            {
                comparisonText += (comparisonValue.ToString() + " " + Unit + " more ");
            }
            float relativeDifference = (comparisonValue / (float)diffValue) - 1;

            if (comparisonValue > diffValue)
            {
                comparisonText += Math.Round((Math.Abs(relativeDifference) * 100)).ToString() + "% more";
            }
            else if (comparisonValue < diffValue)
            {
                comparisonText += (Math.Round((Math.Abs(relativeDifference) * 100))).ToString() + "% less";
            }
            else
            {
                comparisonText += "the same amount of " + Unit;
            }

            comparisonText += $"{ResourceType} than {biggestDiffCountry?.Name}";
            return comparisonText;
        }

        public Country GetCountryWithBiggestDifference(float comparisonValue)
        {
            Country countryWithBiggestDifference = null;
            float biggestDifference = 0;

            foreach (var kvp in ValueMap)
            {
                var countryName = kvp.Key;
                var valueList = kvp.Value;

                var dataPoint = valueList.FirstOrDefault(dp => dp.Date == Date);

                if (dataPoint != null)
                {
                    float difference = (float)Math.Abs(comparisonValue - dataPoint.Value);

                    if (difference > biggestDifference)
                    {
                        biggestDifference = difference;
                        countryWithBiggestDifference = CountryList.FirstOrDefault(c => c.Name == countryName);
                    }
                }
            }

            return countryWithBiggestDifference;
        }

        // Sets width of comparison value bar
        public float SetBarWidth()
        {
            float maxValue = ValueMap.Values.Max(valueList => (float)valueList.Max(dataPoint => dataPoint.Value));
            return maxValue * 1.3f;
        }
    }
}
