namespace Calico.Cmd
{
    using PowerArgs;

    public class NewDataSetArgs
    {
        [ArgRequired]
        [ArgDescription(DefaultArgDescriptions.PlotId)]
        public int PlotId { get; set; }

        [ArgRequired]
        [ArgDescription(DefaultArgDescriptions.FeatureTypeId)]
        public int FeatureTypeId { get; set; }

        [ArgRequired]
        [ArgDescription("The name of the data set")]
        public string Name { get; set; }
    }
}
