namespace API.Contracts
{
    public class DataPoint
    {
        public required DateOnly Date { get; init; }
        public required double Value { get; init; }
    }
}
