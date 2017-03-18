namespace Calico.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class DataSet
    {
        [Key]
        public int Id { get; set; }

        public int PlotId { get; set; }

        public int FeatureTypeId { get; set; }

        public string Name { get; set; }

        public DateTime DateCreated { get; set; }

        public virtual FeatureType FeatureType { get; set; }

        public virtual ICollection<Feature> Features { get; set; }

        public virtual ICollection<AttributeValue> AttributeValues { get; set; }
    }
}
