using System.Collections.ObjectModel;

namespace API
{
    public class Data
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string Unit { get; set; }
        public required Collection<DataPoint> Points { get; init; }

        public override bool Equals(object? obj)
        {
            if (obj == null ||  obj.GetType() != typeof(Data))
                return false;
            Data other = (Data)obj;
            if (this.Points.Count != other.Points.Count)
                return false;
            foreach (DataPoint point in this.Points)
            {
                if (!other.Points.Contains(point)) 
                    return false;
            }
            return (
                this.Name == other.Name &&
                this.Description == other.Description &&
                this.Unit == other.Unit &&
                this.Points.Equals(other.Points)
            ); ;
        }
    }
}
