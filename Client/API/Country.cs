using System.Collections.ObjectModel;
using System.Runtime.Serialization.DataContracts;

namespace API
{
    public class Country
    {
        public required string Code { get; set; }
        public required string Name { get; set; }
        public string? Continent { get; set; }
        public string? Description { get; set; }
        public IList<Data>? Data { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != typeof(Country))
                return false;
            Country other = (Country)obj;
            if ((this.Data == null && other.Data != null) ||
                (this.Data != null && other.Data == null))
            {
                return false;
            }
            else if (this.Data != null && other.Data != null && !this.Data.SequenceEqual(other.Data))
            {
                return false;
            }
            return (
                this.Code == other.Code &&
                this.Name == other.Name &&
                this.Continent == other.Continent &&
                this.Description == other.Description
            );
        }

        public override string ToString()
        {
            return Code;
        }
    }
}
