namespace Calico.Data
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class FeatureType
    {
        [Key]
        public int Id { get; set; }

        public int TenantId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Attribute> Attributes { get; set; }
    }
}
