using Microsoft.AspNetCore.Components;

namespace Client.API
{
    public static class ApiGlobal
    {
        private static string _clientIP { get; set; }

        public static async Task<string> GetClientIPAsync(HttpClient httpClient)
        {
            if (_clientIP != null) return _clientIP;

            try
            {
                var ip = await httpClient.GetStringAsync("https://api.ipify.org/");
                if (ip != null)
                {
                    _clientIP = ip;
                    return _clientIP;
                }
                else
                {
                    throw new Exception("Failed to get IP address");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception($"Api call failed, {e}");
            }
        }
    }
}
