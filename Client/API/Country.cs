using System.Collections.ObjectModel;

namespace API
{
    public class Country
    {
        public required string Code { get; init; }
        public required string Name { get; init; }
        public string? Continent { get; init; }
        public string? Description { get; init; }
        public Collection<Data>? Data { get; init; }
    }
}
