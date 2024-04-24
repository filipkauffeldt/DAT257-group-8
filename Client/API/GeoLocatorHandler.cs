namespace Client.API
{
    public class GeoLocatorHandler : IGeoLocator
    {
        private readonly string apiUrl = "https://ipapi.co/";
        public async Task<string> GetHomeISOAsync(HttpClient httpClient)
        {
            try
            {
                var ip = await ApiGlobal.GetClientIPAsync(httpClient);
                var iso = await httpClient.GetStringAsync($"{apiUrl}/{ip}/country_code_iso3/");
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