// <copyright file="FeatureRecord.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    public class FeatureRecord
    {
        public int DataSetId { get; set; }

        public int Index { get; set; }

        public string Wkt { get; set; }

        public int SRID { get; set; }
    }
}
