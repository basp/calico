// <copyright file="ScanShapefileResponse.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System.Collections.Generic;

    public class ScanShapefileResponse
    {
        public int NumberOfFeatures { get; set; }

        public IEnumerable<AttributeRecord> Attributes { get; set; }

        public IEnumerable<FeatureTypeRecord> FeatureTypes { get; set; }
    }
}
