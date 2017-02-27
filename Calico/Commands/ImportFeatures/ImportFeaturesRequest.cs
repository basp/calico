namespace Calico
{
    public class ImportFeaturesRequest
    {
        public int DataSetId { get; set; }

        public string PathToShapefile { get; set; }

        public int SRID { get; set; }
    }
}
