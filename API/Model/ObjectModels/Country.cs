using System.ComponentModel.DataAnnotations.Schema;

namespace API.Model.ObjectModels
{
    [Table("countries")]
    public class Country
    {
        public required string Code { get; set; }
        public required string Name { get; set; }
        public string? Continent { get; set; }
        public string? Description { get; set; }
        public required IEnumerable<Data> Data { get; set; }
    }
}
