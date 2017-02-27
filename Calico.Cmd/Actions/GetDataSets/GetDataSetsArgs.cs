namespace Calico.Cmd
{
    using PowerArgs;

    public class GetDataSetsArgs
    {
        [ArgRequired]
        public int PlotId { get; set; }

        [ArgDefaultValue(50)]
        public int Top { get; set; }
    }
}
