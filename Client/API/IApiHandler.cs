using API;
using System.Net.Http;

namespace Client.API
{
    public interface IApiHandler
    {
        Task<Country> FetchCountryOfTheDay(HttpClient httpClient);

        Task<Country> FetchCountry(string iso, HttpClient httpClient);

        Task<List<Country>> FetchAllCountries(HttpClient httpClient);
    }
}
