namespace Calico.Data
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Feature
    {
        [Key]
        [Column(Order = 1)]
        public int DataSetId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int Index { get; set; }

        public DbGeometry Geometry { get; set; }

        public int SRID { get; set; }
    }
}
