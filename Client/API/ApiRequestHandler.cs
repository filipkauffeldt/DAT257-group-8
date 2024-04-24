﻿using API;
using Microsoft.AspNetCore.Components;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;


namespace Client.API
{
    public class ApiRequestHandler : IApiHandler
    {
        private string apiUrl = "https://localhost:7262";
        private readonly IGeoLocator geoLocator;

        public ApiRequestHandler(IGeoLocator geoLocator)
        {
            this.geoLocator = geoLocator;
        }

        public async Task<Country> FetchCountryOfTheDay(HttpClient httpClient)
        {
            try
            {
                return await httpClient.GetFromJsonAsync<Country>($"{apiUrl}/Country/GetCountryOfTheDay");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception($"Api call failed, {e}");
            }
        }

        public async Task<Country> FetchCountry(string iso, HttpClient httpClient)
        {
            try
            {
                return await httpClient.GetFromJsonAsync<Country>($"{apiUrl}/Country/GetCountry/{iso}");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception($"Api call failed, {e}");
            }
        }

        public async Task<IEnumerable<Country>> FetchAllCountries(HttpClient httpClient)
        {
            try
            {
                return await httpClient.GetFromJsonAsync<IEnumerable<Country>>($"{apiUrl}/Country/GetAllCountries");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception($"Api call failed, {e}");
            }
        }

        public async Task<Country> FetchCountryByYear(HttpClient httpClient, string code, DateOnly year)
        {
            try
            {
                return await httpClient.GetFromJsonAsync<Country>($"{apiUrl}/Country/GetCountryDataForYear/{code}/{year}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception($"Api call failed, {e}");
            }
        }
        public async Task<Country> FetchHomeCountry(HttpClient httpClient)
        {
            var iso = await geoLocator.GetHomeISOAsync(httpClient);
            return await FetchCountry(iso, httpClient);
        }
    }
}