namespace Calico.Cmd
{
    using PowerArgs;

    public class NewFeatureTypeArgs
    {
        [ArgRequired]
        [ArgDescription(DefaultArgDescriptions.ClientId)]
        public int ClientId { get; set; }

        [ArgRequired]
        [ArgDescription("The name of the feature type")]
        public string Name { get; set; }
    }
}
