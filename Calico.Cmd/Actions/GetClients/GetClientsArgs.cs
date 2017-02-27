namespace Calico.Cmd
{
    using PowerArgs;

    public class GetClientsArgs
    {
        [ArgDefaultValue(50)]
        [ArgDescription(DefaultArgDescriptions.Top)]
        public int Top { get; set; }
    }
}
