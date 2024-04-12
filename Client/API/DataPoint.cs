namespace API
{
    public class DataPoint
    {
        public required string Key { get; init; }
        public required double Value { get; init; }

        public override bool Equals(object? obj)
        {
            if (obj == null ||  obj.GetType() != typeof(DataPoint))
            {
                return false;
            }
            DataPoint other = (DataPoint)obj;
            return (
                this.Key == other.Key &&
                this.Value == other.Value
            );
        }
    }
}
