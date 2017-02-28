namespace Calico.Cmd
{
    using PowerArgs;

    public class ImportAttributesArgs
    {
        [ArgRequired]
        [ArgDescription(DefaultArgDescriptions.FeatureTypeId)]
        public int FeatureTypeId { get; set; }

        [ArgRequired]
        [ArgDescription(DefaultArgDescriptions.Shapefile)]
        public string PathToShapefile { get; set; }
    }
}
