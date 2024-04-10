using Microsoft.AspNetCore.Components;

using API.Contracts;
using System.Linq;

namespace Client.Components
{
    public partial class ComparisonComponent
    {
        //TODO: Make them required 
        [Parameter]
        public Country? CountryOrigin { get; set; } 
        [Parameter]
        public Country? CountryComparison{get; set;}
        [Parameter]
        public string ResourceType { get; set; } = "NaN";


        public float OriginCountryValue { get; set; } = 1.0f;
        public float ComparisonCountryValue { get; set; } = 1.0f;
        public string Unit { get; set; } = "NaN";
        public string OriginCountryName { get; set; } = "Sweden";
        public string ComparisonCountryName { get; set; } = "Nauru";
        
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
        
        private string DifferencePercentage()
        {
            
            //TODO: remove once country is made required, 
            if(CountryComparison != null && CountryOrigin != null)
            {
                OriginCountryName = CountryOrigin.Name;
                var resource1 = GetCountryData(CountryOrigin);
                ComparisonCountryName = CountryComparison.Name;
                var resource2 = GetCountryData(CountryComparison);

                if (resource1 != null && resource2 != null)
                {
                    //TODO Find resource data and add
                }
                else
                {
                    return "No data available";
                }
            }
            
            
            
            
            
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
