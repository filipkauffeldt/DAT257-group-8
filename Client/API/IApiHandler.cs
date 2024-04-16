using API;
using System.Net.Http;

namespace Client.API
{
    public interface IApiHandler
    {
        Task<Country> FetchCountryOfTheDay(HttpClient httpClient);

        Task<Country> FetchCountry(string iso, HttpClient httpClient);

        Task<IEnumerable<Country>> FetchAllCountries(HttpClient httpClient);

        Task<Country> FetchCountryDataByTimeSpan(HttpClient httpClient, string code, DateOnly minDate, DateOnly maxDate);
    }
}
