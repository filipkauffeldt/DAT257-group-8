namespace API.Contracts
{
    public class DataPoint
    {
        public required DateTime Date { get; init; }
        public required double Value { get; init; }
    }
}
