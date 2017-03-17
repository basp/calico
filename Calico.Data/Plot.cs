namespace Calico.Data
{
    using System.Collections.Generic;
    using System.Data.Entity.Spatial;

    public class Plot
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public string Name { get; set; }

        public DbGeometry Geometry { get; set; }

        public virtual ICollection<DataSet> DataSets { get; set; }
    }
}
