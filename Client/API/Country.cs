using System.Collections.ObjectModel;

namespace API
{
    public class Country
    {
        public required string Code { get; init; }
        public required string Name { get; init; }
        public string? Continent { get; init; }
        public string? Description { get; init; }
        public Collection<Data>? Data { get; init; }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != typeof(Country))
                return false;
            Country other = (Country)obj;
            if (this.Data == null && other.Data != null ||
                this.Data != null && other.Data == null)
            {
                return false;
            }
            else if (this.Data != null && other.Data != null && !this.Data.Equals(other.Data))
                return false;
            return (
                this.Code == other.Code &&
                this.Name == other.Name &&
                this.Continent == other.Continent &&
                this.Description == other.Description
            );
        }
    }
}
