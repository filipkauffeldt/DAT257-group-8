using Microsoft.AspNetCore.Components;

using API;
using System.Linq;
using Radzen.Blazor;
using Radzen;


namespace Client.Components
{

    public partial class ComparisonComponent
    {
        [Parameter]
        public EventCallback OnClick { get; set;  }
        [Parameter]
        public required Country OriginCountry { get; set; }

        [Parameter]
        public required IList<Country> ComparedCountries { get; set;}

        [Parameter]
        public string ResourceType { get; set; } = "NaN";

        [Parameter]
        public required DateOnly Date {  get; set; }

        private readonly IDictionary<Country, IList<DataPoint>> ValueMap = new Dictionary<Country, IList<DataPoint>>();

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
            if (OnClick.HasDelegate == false)
            {
                OnClick = EventCallback.Factory.Create(this, OpenBiggerViewOfGraph);
            }
        }

        // Update values on parameter change
        protected override void OnParametersSet()
        {
            LoadValues();
            StateHasChanged();
        }

        public void LoadValues()
        {
            var countries = new List<Country> { OriginCountry };
            countries.AddRange(ComparedCountries);

            foreach (var country in countries)
            {
                var resource = GetCountryData(country);
                ValueMap.Remove(country);
                if (resource != null)
                {
                    var valueList = new List<DataPoint> {
                        new DataPoint { Date = Date, Value = resource.Points.Where(dp => dp.Date.Year == Date.Year).FirstOrDefault()?.Value ?? 1 }
                    };
                    ValueMap.Add(country, valueList);
                    Unit = resource.Unit;
                }
                else
                {
                    var valueList = new List<DataPoint> {
                        new DataPoint { Date = Date, Value = 1 }
                    };
                    ValueMap.Add(country, valueList);
                }
            }
            
            // Text at the bottom of the cards
            ConsumptionText = "Consumption in " + Unit;
        }

        public string GetComparisonText()
        {
            if (ComparedCountries.Count == 0)
            {
                return "No comparison data available";
            }
            else if (ComparedCountries.Count == 1)
            {
                var comparedCountry = ComparedCountries[0];
                var comparedCountryValue = ValueMap[comparedCountry][0].Value;
                var originCountryValue = ValueMap[OriginCountry][0].Value;
                var resourceDescription = GetCountryData(OriginCountry)?.Description ?? ResourceType;
                return SingleComparedText(OriginCountry, comparedCountry, originCountryValue, comparedCountryValue, resourceDescription, Unit);
            }
            else
            {
                var singleValueMap = ValueMap.ToDictionary(kvp => kvp.Key, kvp => kvp.Value[0].Value);
                var resourceDescription = GetCountryData(OriginCountry)?.Description ?? ResourceType;
                return MultipleComparedText(OriginCountry, singleValueMap, resourceDescription, Unit);
            }
        }

        public string MultipleComparedText(Country originCountry, IDictionary<Country, double> valueMap, string resource, string unit)
        {
            var comparisonText = $"{originCountry.Name} uses ";
            var originValue = valueMap[originCountry];
            var descendingValues = valueMap.OrderByDescending(kvp => kvp.Value).ToList();
            var ascendingValues = valueMap.OrderBy(kvp => kvp.Value).ToList();
            
            var avrageValue = valueMap.Where(kvp => kvp.Key != originCountry).Average(kvp => kvp.Value);

            if (Math.Abs(originValue - avrageValue) < Threshold)
            {
                comparisonText += $"the same amount of {resource} as the average";
            }
            else if (originValue > avrageValue)
            {
                if (originValue < Threshold || avrageValue < Threshold)
                {
                    comparisonText += $"{Math.Abs(Math.Round(originValue - avrageValue, 5))} {unit} more {resource} than the average";
                }
                else
                {
                    comparisonText += $"{Math.Round((originValue / avrageValue - 1) * 100)}% more {resource} than the average";
                }
            }
            if (originValue < avrageValue)
            {
                if (originValue < Threshold || avrageValue < Threshold)
                {
                    comparisonText += $"{Math.Abs(Math.Round(originValue - avrageValue, 5))} {unit} less {resource} than the average";
                }
                else
                {
                    comparisonText += $"{Math.Round(Math.Abs(originValue / avrageValue - 1) * 100)}% less {resource} than the average";
                }
            }

            return comparisonText;
        }

        public string SingleComparedText(Country originCountry, Country comparedCountry, double originValue, double comparedValue, string resource, string unit)
        {
            string comparisonText = $"{originCountry.Name} use ";

            var diffValue = Math.Abs(originValue - comparedValue);
            var smallValueDetected = Math.Abs(originValue) < Threshold || Math.Abs(comparedValue) < Threshold;

            if (diffValue < Threshold)
            {
                comparisonText += $"the same amount of {resource} as {comparedCountry.Name}";
                return comparisonText;
            }
            
            
            var relativeDifference = Math.Abs((originValue / comparedValue));

            if (relativeDifference > 1)
            {
                if (smallValueDetected)
                {
                    comparisonText += $"{Math.Round(diffValue, 2)} {unit} more ";
                }
                else
                {
                    comparisonText += Math.Round((relativeDifference - 1) * 100).ToString() + "% more ";
                }
            }
            else if (relativeDifference < 1)
            {
                if (smallValueDetected)
                {
                    comparisonText += $"{Math.Round(diffValue, 2)} {unit} less ";
                }
                else
                {
                    comparisonText += Math.Round((1 - relativeDifference) * 100).ToString() + "% less ";
                }
            }
            else
            {
                comparisonText += $"the same amount of {resource} as {comparedCountry.Name}";
                return comparisonText;
            }

            comparisonText += $"{resource} than {comparedCountry?.Name}";
            return comparisonText;
        }

        // Sets width of comparison value bar
        public float SetBarWidth()
        {
            float maxValue = ValueMap.Values.Max(valueList => (float)valueList.Max(dataPoint => dataPoint.Value));
            if(maxValue == 0)
            {
                return 1;
            }
            return maxValue * 1.3f;
        }

        public async Task OpenBiggerViewOfGraph()
        {
			await DialogService.OpenAsync<CustomComparisonModal>("",
                new Dictionary<string, object>() { { "ComparedCountries", ComparedCountries },
                                                   { "OriginCountry", OriginCountry },
                                                   { "ResourceType", ResourceType },
                                                   { "Date", Date }},
                new DialogOptions() {Width = "1000px", Height = "700px", CloseDialogOnOverlayClick = true});
        }
    }
}
