namespace Calico.Cmd
{
    using PowerArgs;

    public class GetAttributesArgs
    {
        [ArgRequired]
        [ArgDescription(DefaultArgDescriptions.FeatureTypeId)]
        public int FeatureTypeId { get; set; }
    }
}
