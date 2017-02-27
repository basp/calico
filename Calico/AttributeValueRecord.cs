namespace Calico
{
    public class AttributeValueRecord
    {
        public int DataSetId { get; set; }

        public int AttributeId { get; set; }

        public int FeatureIndex { get; set; }

        public double DoubleValue { get; set; }

        public long LongValue { get; set; }

        public string StringValue { get; set; }
    }
}
