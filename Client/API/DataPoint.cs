namespace API
{
    public class DataPoint
    {
        public required DateOnly Date { get; set; }
        public required double Value { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null ||  obj.GetType() != typeof(DataPoint))
            {
                return false;
            }
            DataPoint other = (DataPoint)obj;
            return (
                this.Date == other.Date &&
                this.Value == other.Value
            );
        }
    }
}
