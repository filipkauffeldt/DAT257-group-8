using System.Collections.ObjectModel;

namespace API
{
    public class Data
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Unit { get; set; }
        public IList<DataPoint> Points { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj is not Data other)
                return false;

            if (this.Points.Count != other.Points.Count)
                return false;

            for (int i = 0; i < this.Points.Count; i++)
            {
                if (!this.Points[i].Equals(other.Points[i]))
                    return false;
            }

            return (
                this.Name == other.Name &&
                this.Description == other.Description &&
                this.Unit == other.Unit
            );
        }
    }
}
