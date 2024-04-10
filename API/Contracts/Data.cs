using System.Collections.ObjectModel;

namespace API.Contracts
{
    public class Data
    {
        public required string Name { get; init; }
        public string? Description { get; init; }
        public string? Unit {  get; init; }
        public required Collection<DataPoint> Points { get; init; }
    }
}
