using System.Collections.ObjectModel;

namespace API.Contracts
{
    public class Data
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required Collection<DataPoint> Points { get; set; }
    }
}
