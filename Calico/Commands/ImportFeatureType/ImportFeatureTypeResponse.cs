// <copyright file="ImportFeatureTypeResponse.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System.Collections.Generic;

    public class ImportFeatureTypeResponse
    {
        public TenantRecord Tenant { get; set; }

        public FeatureTypeRecord FeatureType { get; set; }

        public IEnumerable<AttributeRecord> Attributes { get; set; }
    }
}
