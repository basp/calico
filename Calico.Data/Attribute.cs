namespace Calico.Data
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Attribute
    {
        [Key]
        [Column(Order = 1)]
        public int FeatureTypeId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int Index { get; set; }

        public int DataTypeId { get; set; }

        public string Name { get; set; }

        public DataType DataType { get; set; }
    }
}
