namespace Calico.Cmd
{
    using PowerArgs;

    public class ImportFeaturesArgs
    {
        [ArgRequired]
        [ArgDescription(DefaultArgDescriptions.DataSetId)]
        public int DataSetId { get; set; }

        [ArgRequired]
        [ArgDescription(DefaultArgDescriptions.Shapefile)]
        public string PathToShapefile { get; set; }

        [ArgDefaultValue(4326)]
        [ArgDescription(DefaultArgDescriptions.SpatialReferenceSystem)]
        public int SRID { get; set; }
    }
}
