namespace Client.API
{
    public interface IGeoLocator
    {
        Task<string> GetUserISOAsync(HttpClient httpClient);
    }
}
