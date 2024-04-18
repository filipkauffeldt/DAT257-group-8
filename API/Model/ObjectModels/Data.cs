using System.ComponentModel.DataAnnotations.Schema;

namespace API.Model.ObjectModels
{
    [Table("data")]
    public class Data
    {
        public long D_Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string Unit {  get; set; }
        public required IList<DataPoint> Points { get; set; }
    }
}
