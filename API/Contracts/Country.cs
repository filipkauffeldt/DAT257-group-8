using System.Collections.ObjectModel;

namespace API.Contracts
{
    public class Country
    {
        public required string Code { get; set; }
        public required string Name { get; set; }
        public string? Continent { get; set; }
        public string? Description { get; set; }
        public Collection<Data>? Data { get; set; }

    }
}
