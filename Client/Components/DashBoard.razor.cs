using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using API;
using Client.API;
using Microsoft.AspNetCore.Components;

namespace Client.Components
{
    public partial class DashBoard
    {
        public string test;
        private HomeCountryDropDown dropDown;
        private ComparisonComponent comparisonComponent;
        private List<ComparisonComponent> comparisonComponents = new();
        private List<string> countryNames;

        private List<ComparisonComponent> _refs = new();
        //public CompareAttribute Ref { set => _refs.Add(value); }
        private Dictionary<string, ComparisonComponent> compComp = new Dictionary<string, ComparisonComponent>();
        private Dictionary<string, string> countryCodeDict = new();

        [Inject]
        private HttpClient httpClient { get; set; }

        [Inject]
        private IApiHandler apiHandler { get; set; }

        private Country countryComp { get; set; }

        private Country countryCompTwo { get; set; }

        DateOnly date = new DateOnly(2022, 1, 1);
        private IList<string> dataMetrics = new List<string>();




        protected override async Task OnInitializedAsync()
        {
            countryComp = await apiHandler.FetchCountryByYear(httpClient, "SWE", date); // Sweden
            countryCompTwo = await apiHandler.FetchCountryByYear(httpClient, "BGR", date); // Bulgaria
            dataMetrics = GetValidMetrics();
            var allCountries = await apiHandler.FetchAllCountries(httpClient);
            foreach(Country country in allCountries)
            {
                countryCodeDict.Add(country.Name, country.Code);
            }
            var countryTest = await apiHandler.FetchCountryByYear(httpClient, "URY", date);
            var DataTest = countryTest.Data.Where(d => d.Name == "el-coal").First();
            test = DataTest.Points.Where(dp => dp.Date.Year == date.Year).FirstOrDefault().Value.ToString();
            countryNames = countryCodeDict.Keys.ToList();
        }


        private async void HomeCountryChange(string CountryCode)
        {
            countryCompTwo = await apiHandler.FetchCountry(CountryCode, httpClient);

            StateHasChanged();
            foreach (var cC in compComp.Values)
            {
                cC.LoadValues();
            }
            StateHasChanged();
        }

        private IList<string> GetValidMetrics()
        {
            if (countryComp == null || countryComp.Data == null ||
                countryCompTwo == null || countryCompTwo.Data == null) {
                return new List<string>();
            }

            var validMetrics = new List<string>();
            foreach (var metric in countryComp.Data.Select(d => d.Name).ToList())
            {
                var countryCompDataExists = countryComp.Data?.Any(d => d.Name == metric && d.Points.Any()) ?? false;
                var countryCompTwoDataExists = countryCompTwo.Data?.Any(d => d.Name == metric && d.Points.Any()) ?? false;

                if (countryCompDataExists && countryCompTwoDataExists)
                {
                    validMetrics.Add(metric);
                }
            }

            return validMetrics;
        }

        private static string FormatLabel(string label)
        {
            return $"{label.Replace(" ", "-")}-statistic";
        }
    }
}
