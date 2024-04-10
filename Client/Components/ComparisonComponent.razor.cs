using Microsoft.AspNetCore.Components;

using API.Contracts;
using System.Linq;

namespace Client.Components
{
    public partial class ComparisonComponent
    {
        //Using API.Contracts
        [Parameter]
        public required List<Country> Countries { get; set; }
        [Parameter]
        public required string ResourceType { get; set; }

        
        public required float OriginCountryValue { get; set; }
        public required float ComparisonCountryValue { get; set; }
        public required string Unit { get; set; }
        public required string OriginCountry { get; set; } = "Sweden";
        public required string ComparisonCountry { get; set; } = "Nauru";
        

        private string DifferencePercentage()
        {
            OriginCountryValue = Countries[0].Data.Points.Value
            var data = Countries.Where(c => c.Code == "SWE").FirstOrDefault()?.Data;
            var resource = Countries[0].Data.Where(d => d.Name == ResourceType);
            if (ComparisonCountryValue > OriginCountryValue)
            {
                return ((ComparisonCountryValue - OriginCountryValue) / OriginCountryValue * 100).ToString("n2") + "% more";
            }
            else if (ComparisonCountryValue < OriginCountryValue)
            {
                return ((OriginCountryValue - ComparisonCountryValue) / OriginCountryValue * 100).ToString("n2") + "% less";
            }
            else
            {
                return "the same amount of";
            }
        }
    }
}
