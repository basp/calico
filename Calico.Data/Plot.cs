namespace Calico.Data
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity.Spatial;

    public class Plot
    {
        [Key]
        public int Id { get; set; }

        public int TenantId { get; set; }

        public string Name { get; set; }

        public DbGeometry Geometry { get; set; }

        public virtual ICollection<DataSet> DataSets { get; set; }
    }
}
