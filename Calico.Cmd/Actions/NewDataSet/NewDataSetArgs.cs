namespace Calico.Cmd
{
    using PowerArgs;

    public class NewDataSetArgs
    {
        [ArgRequired]
        public int PlotId { get; set; }

        [ArgRequired]
        public int FeatureTypeId { get; set; }

        [ArgRequired]
        public string Name { get; set; }
    }
}
