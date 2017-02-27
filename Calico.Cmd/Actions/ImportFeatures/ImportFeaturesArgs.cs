namespace Calico.Cmd
{
    using PowerArgs;

    public class ImportFeaturesArgs
    {
        [ArgRequired]
        public int DataSetId { get; set; }

        [ArgRequired]
        public string PathToShapefile { get; set; }
    }
}
