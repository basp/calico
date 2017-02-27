namespace Calico
{
    using PowerArgs;

    public class NewPlotRequest
    {
        [ArgRequired]
        public int ClientId { get; set; }

        [ArgRequired]
        public string PathToShapefile { get; set; }

        [ArgRequired]
        public string Name { get; set; }

        [ArgDefaultValue(4326)]
        public int SRID { get; set; }
    }
}
