namespace Client.API
{
    public class GeoLocatorHandler : IGeoLocator
    {
        private readonly string _apiUrl = "https://ipapi.co";
        public async Task<string> GetUserISOAsync(HttpClient httpClient)
        {
            return await new RequestWrapper<string>().GetStringAsync(httpClient, $"{_apiUrl}/country_code_iso3/");
        }
    }
}