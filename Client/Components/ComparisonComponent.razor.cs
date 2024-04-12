﻿using Microsoft.AspNetCore.Components;

using API.Contracts;
using System.Linq;

namespace Client.Components
{
    public partial class ComparisonComponent
    {
        //TODO: Make them required 
        [Parameter]
        public required Country CountryOrigin { get; set; } 
        [Parameter]
        public required Country CountryComparison{get; set;}
        [Parameter]
        public string ResourceType { get; set; } = "NaN";

        [Parameter]
        public DateOnly Date {  get; set; }

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
                    // Supposed to get the data from the year 2023, assuming that the key is "2023". #TODO make it better, autonomous,dependency liksom
                    OriginCountryValue = (float)resource1.Points.Where(dp => dp.DateTime == "2023").FirstOrDefault().Value;
                    ComparisonCountryValue = (float)resource2.Points.Where(dp => dp.DateTime == "2023").FirstOrDefault().Value;
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
                return (relativeDifference * 100).ToString("n2") + "% more";
            }
            else if (ComparisonCountryValue < OriginCountryValue)
            {
                return (relativeDifference * 100).ToString("n2") + "% less";
            }
            else
            {
                return "the same amount of";
            }
        }
    }
}
