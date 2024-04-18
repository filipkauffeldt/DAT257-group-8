using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using API;
using Client.API;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen.Blazor;


namespace Client.Components
{
    public partial class DashBoard
    {
        private Country? countryOfTheDay;
        private List<Country> comparedCountries = new List<Country>();
        private Country countryComp = new Country()
        {
            Name = "Bulgaria",
            Code = "BGR",
            Data = [new Data() { Name = "Water", Unit = "L", Points = [new DataPoint() { Date = new DateOnly(2023,1,1), Value = 100}] },
                    new Data() {Name = "CO2 Emissions", Unit = "Kg", Points = [new DataPoint() { Date = new DateOnly(2023, 1, 1), Value = 200 }]}]
        };
        private Country countryCompTwo = new Country()
        {
            Name = "Sweden",
            Code = "SWE",
            Data = [new Data() { Name = "Water", Unit = "L", Points = [new DataPoint() { Date = new DateOnly(2023, 1, 1), Value = 200 }] },
                    new Data() {Name = "CO2 Emissions", Unit = "Kg", Points = [new DataPoint() { Date = new DateOnly(2023, 1, 1), Value = 200 }]}]
        };
        DateOnly date = new DateOnly(2020, 1, 1);
        // Dummy labels
        private readonly string[] dataLabels = { "Water", "Electricity", "CO2" };
        private IList<string> dataMetrics = new List<string>();

        protected override async Task OnInitializedAsync()
        {
            countryComp = await apiHandler.FetchCountryByYear(httpClient, countryComp.Code, date);
            countryCompTwo = await apiHandler.FetchCountryByYear(httpClient, countryCompTwo.Code, date);
            dataMetrics = GetSharedMetrics();
        }

        private IList<string> GetSharedMetrics()
        {
            if (countryComp == null || countryComp.Data == null ||
                countryCompTwo == null || countryCompTwo.Data == null) {
                return new List<string>();
            }

            //var countryComp2Metrics = new HashSet<Tuple<string, int>>(
            //    countryComp.Data.Select(d => d.Points.)    
            //);

            //var sharedMetrics = countryComp.Data
            //    .Select(data => data.Name)
            //    .Intersect(countryCompTwo.Data.Select(data => data.Name))
            //    .ToList();

            //return sharedMetrics;

            //var sharedMetrics = countryComp.Data
            //    .Where(data => countryCompTwo.Data.Any(d => d.Name == data.Name))
            //    .Select(data => new Data
            //    {
            //        Name = data.Name,
            //        Unit = data.Unit,
            //        Points = data.Points.Where(point => countryCompTwo.Data.Any(d => d.Name == data.Name && d.Points.Any(p => p.Date.Year == point.Date.Year))).ToList()
            //    })
            //    .ToList();

            //var sharedMetrics = new Dictionary<string, HashSet<int>>();
            //foreach (var data in countryCompTwo.Data)
            //{
            //    var points = new HashSet<int>(data.Points.Select(p => p.Date.Year));
            //    sharedMetrics.Add(data.Name, points);
            //}

            //foreach (var data in countryComp.Data)
            //{
            //    if (sharedMetrics.ContainsKey(data.Name))
            //    {
            //        sharedMetrics[data.Name].IntersectWith(data.Points.Select(p => p.Date.Year));
            //    }
            //}

            //countryComp.Data = countryComp.Data
            //    .Select(data => new Data
            //    {
            //        Name = data.Name,
            //        Unit = data.Unit,
            //        Points = data.Points.Where(p => sharedMetrics.ContainsKey(data.Name) && sharedMetrics[data.Name].Contains(p.Date.Year)).ToList()
            //    })
            //    .ToList();

            //countryCompTwo.Data = countryCompTwo.Data
            //    .Select(data => new Data
            //    {
            //        Name = data.Name,
            //        Unit = data.Unit,
            //        Points = data.Points.Where(p => sharedMetrics.ContainsKey(data.Name) && sharedMetrics[data.Name].Contains(p.Date.Year)).ToList()
            //    })
            //    .ToList();

            return countryComp.Data.Select(d => d.Name).ToList();
        }

        private static string FormatLabel(string label)
        {
            return $"{label.Replace(" ", "-")}-statistic";
        }
    }
}
