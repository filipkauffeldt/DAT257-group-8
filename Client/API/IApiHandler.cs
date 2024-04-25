using API;
using System.Net.Http;

namespace Client.API
{
    public interface IApiHandler
    {
        Task<Country> FetchCountryOfTheDay(HttpClient httpClient);

        Task<Country> FetchHomeCountry(HttpClient httpClient);

        Task<Country> FetchCountry(string iso, HttpClient httpClient);

        Task<IEnumerable<Country>> FetchAllCountries(HttpClient httpClient);

        Task<Country> FetchCountryByYear(HttpClient httpClient, string code, DateOnly year);

        Task<IEnumerable<string>> FetchAllCountryNames(HttpClient httpClient);

        Task<IEnumerable<string>> FetchAllCountryCodes(HttpClient httpClient);

        Task<IDictionary<string, string>> FetchAllCountryNamesDict(HttpClient httpClient);
    }
}
