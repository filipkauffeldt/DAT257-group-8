using System.Collections.ObjectModel;

namespace API.Contracts
{
    public class CountryContract
    {
        public required string Code { get; init; }
        public required string Name { get; init; }
        public string? Continent { get; init; }
        public string? Description { get; init; }
        public Collection<DataContract>? Data { get; init; }

    }
}
