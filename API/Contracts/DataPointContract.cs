namespace API.Contracts
{
    public class DataPointContract
    {
        public required DateOnly Date { get; init; }
        public required double Value { get; init; }
    }
}
