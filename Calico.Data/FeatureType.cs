﻿namespace Calico.Data
{
    using System.Collections.Generic;

    public class FeatureType
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Attribute> Attributes { get; set; }
    }
}
