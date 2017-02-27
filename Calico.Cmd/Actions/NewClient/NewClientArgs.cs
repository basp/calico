namespace Calico.Cmd
{
    using PowerArgs;

    public class NewClientArgs
    {
        [ArgRequired]
        public string Name { get; set; }
    }
}
