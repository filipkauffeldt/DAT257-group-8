namespace API
{
    public class DataPoint
    {
        public required DateOnly Date { get; set; }
        public required double Value { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is DataPoint other))
            {
                return false;
            }
            return Date.Equals(other.Date) && Value.Equals(other.Value);
        }
    }
}
