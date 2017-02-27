namespace Calico
{
    using System;

    public class NewDataSetRequest
    {
        public int PlotId { get; set; }

        public int FeatureTypeId { get; set; }

        public string Name { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
