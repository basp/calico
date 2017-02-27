namespace Calico
{
    using PowerArgs;

    public class NewClientRequest
    {
        [ArgRequired]
        public string Name { get; set; }
    }
}
