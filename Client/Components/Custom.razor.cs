﻿using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using API;
using Client.API;
using Microsoft.AspNetCore.Components;

namespace Client.Components
{
    public partial class Custom
    {
        [Inject]
        private HttpClient httpClient { get; set; }

        [Inject]
        private IApiHandler apiHandler { get; set; }

        private string selectedHome = "";

        private string selectedOther = "";

        private Country _country { get; set; }
        
        private Country _countryToCompareWith { get; set; }

        private IList<string> availableCountries = new List<string>() {"SWE", "BGR", "NOR", "DNK", "FIN", "ISL"};
        
        private DateOnly _date = new DateOnly(2022, 1, 1);
        private IList<string> _dataMetrics = new List<string>();

        private IList<string> _availableMetrics = new List<string>();

        protected override async Task OnInitializedAsync()
        {
            _country = await apiHandler.FetchCountryByYear(httpClient, "SWE", _date); // Sweden
            _countryToCompareWith = await apiHandler.FetchCountryByYear(httpClient, "BGR", _date); // Bulgaria
            selectedHome = _country.Code;
            selectedOther = _countryToCompareWith.Code;
            _dataMetrics = GetValidMetrics();
            _availableMetrics = _dataMetrics;
        }

        private IList<string> GetValidMetrics()
        {
            if (_country == null || _country.Data == null ||
                _countryToCompareWith == null || _countryToCompareWith.Data == null) {
                return new List<string>();
            }

            var validMetrics = new List<string>();
            foreach (var metric in _country.Data.Select(d => d.Name).ToList())
            {
                var countryCompDataExists = _country.Data?.Any(d => d.Name == metric && d.Points.Any()) ?? false;
                var countryCompTwoDataExists = _countryToCompareWith.Data?.Any(d => d.Name == metric && d.Points.Any()) ?? false;

                if (countryCompDataExists && countryCompTwoDataExists)
                {
                    validMetrics.Add(metric);
                }
            }

            return validMetrics;
        }

        private async void UpdateHomeCountry(string code)
        {
            selectedHome = code;
            _country = await apiHandler.FetchCountryByYear(httpClient, code, _date);
            _dataMetrics = GetValidMetrics();
            StateHasChanged();
        }

        private async void UpdateOtherCountry(string code)
        {
            selectedOther = code;
            _countryToCompareWith = await apiHandler.FetchCountryByYear(httpClient, code, _date);
            _dataMetrics = GetValidMetrics();
            StateHasChanged();
        }

        private static string FormatLabel(string label)
        {
            return $"{label.Replace(" ", "-")}-statistic";
        }

        private void HandleFilterChange(IEnumerable<string> selectedValues)
        {
            _dataMetrics = selectedValues.ToList();

            // Makes sure that when you select unselected values, they end up in the same order as before
            _dataMetrics = _dataMetrics.OrderBy(d => _availableMetrics.IndexOf(d)).ToList();
        }
    }
}
