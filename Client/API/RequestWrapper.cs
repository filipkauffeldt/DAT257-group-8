using API;
using System.Net.Http.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API
{
    public class RequestWrapper<T>
    {
        public async Task<T> GetFromJSONAsync(HttpClient httpClient, string url)
        {
            try
            {
                var response = await httpClient.GetAsync(url);
                var parsedResponse = await response.Content.ReadFromJsonAsync<T>();
                if (parsedResponse == null)
                {
                    throw new Exception($"Request returned null");
                }
                return parsedResponse;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception($"Api call failed, {e}");
            }
        }

        public async Task<string> GetStringAsync(HttpClient httpClient, string url)
        {
            try
            {
                var response = await httpClient.GetStringAsync(url);
                if (response == null)
                {
                    throw new Exception($"Request returned null");
                }
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception($"Api call failed, {e}");
            }
        }
    }
}
