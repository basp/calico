namespace Calico.Cmd
{
    using PowerArgs;

    public class GetClientsArgs
    {
        [ArgDefaultValue(50)]
        public int Top { get; set; }
    }
}
