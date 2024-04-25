namespace Client.API
{
    public class GeoLocatorHandler : IGeoLocator
    {
        private readonly string _apiUrl = "https://ipapi.co";
        public async Task<string> GetUserISOAsync(HttpClient httpClient)
        {
            try
            {
                var iso = await httpClient.GetStringAsync($"{_apiUrl}/country_code_iso3/");
                return iso;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception($"Api call failed, {e}");
            }
        }
    }
}