namespace Client.API
{
    public interface IGeoLocator
    {
        Task<string> GetHomeISOAsync(HttpClient httpClient);
    }
}
