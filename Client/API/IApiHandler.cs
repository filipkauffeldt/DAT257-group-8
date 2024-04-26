using API;
using System.Net.Http;

namespace Client.API
{
    public interface IApiHandler
    {
        Task<Country> FetchCountryOfTheDayAsync(HttpClient httpClient);

        Task<Country> FetchHomeCountryAsync(HttpClient httpClient);

        Task<Country> FetchCountryAsync(string iso, HttpClient httpClient);

        Task<IEnumerable<Country>> FetchAllCountriesAsync(HttpClient httpClient);

        Task<Country> FetchCountryByYearAsync(HttpClient httpClient, string code, DateOnly year);
    }
}
