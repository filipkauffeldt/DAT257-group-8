namespace API.Contracts
{
    public class DataPoint
    {
        public required string DateTime { get; init; }
        public required double Value { get; init; }
    }
}
