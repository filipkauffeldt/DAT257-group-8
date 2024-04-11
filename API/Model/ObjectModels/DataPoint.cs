using System.ComponentModel.DataAnnotations.Schema;

namespace API.Model.ObjectModels
{
    [Table("datapoints")]
    public class DataPoint
    {
        public long DP_Id { get; set; }
        public required DateOnly Date { get; set; }
        public required double Value { get; set; }
    }
}
