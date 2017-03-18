namespace Calico.Data
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class AttributeValue
    {
        [Key]
        [Column(Order = 0)]
        public int DataSetId { get; set; }

        [Key]
        [Column(Order = 1)]
        public int FeatureIndex { get; set; }

        [Key]
        [Column(Order = 2)]
        public int AttributeIndex { get; set; }

        public double? DoubleValue { get; set; }

        public long? LongValue { get; set; }

        public string StringValue { get; set; }
    }
}
