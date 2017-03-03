// <copyright file="ScanShapefileResponse.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System.Collections.Generic;

    public class ScanShapefileResponse
    {
        public string PathToShapefile { get; set; }

        public int NumberOfFeatures { get; set; }

        public IEnumerable<ScannedAttribute> Attributes { get; set; }

        public IEnumerable<FeatureTypeRecord> FeatureTypes { get; set; }

        public IEnumerable<PlotRecord> Plots { get; set; }
    }
}
