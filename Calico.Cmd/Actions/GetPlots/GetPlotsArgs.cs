namespace Calico.Cmd
{
    using PowerArgs;

    public class GetPlotsArgs
    {
        [ArgRequired]
        [ArgPosition(1)]
        [ArgDescription(DefaultArgDescriptions.ClientId)]
        public int ClientId { get; set; }

        [ArgDefaultValue(50)]
        [ArgDescription(DefaultArgDescriptions.Top)]
        public int Top { get; set; }
    }
}
