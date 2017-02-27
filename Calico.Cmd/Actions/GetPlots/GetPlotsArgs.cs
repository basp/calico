namespace Calico.Cmd
{
    using PowerArgs;

    public class GetPlotsArgs
    {
        [ArgRequired]
        public int ClientId { get; set; }

        [ArgDefaultValue(50)]
        public int Top { get; set; }
    }
}
