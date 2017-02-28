// <copyright file="FeatureRecord.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using Microsoft.SqlServer.Types;

    public class FeatureRecord
    {
        public int DataSetId { get; set; }

        public int Index { get; set; }

        public SqlGeometry Geometry { get; set; }
    }
}
