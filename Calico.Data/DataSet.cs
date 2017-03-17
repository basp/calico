namespace Calico.Data
{
    using System;

    public class DataSet
    {
        public int Id { get; set; }

        public int PlotId { get; set; }

        public int FeatureTypeId { get; set; }

        public string Name { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
