// <copyright file="GetDataSetResponse.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System.Collections.Generic;

    public class GetDataSetResponse
    {
        public DataSetRecord DataSet { get; set; }

        public FeatureTypeRecord FeatureType { get; set; }

        public IEnumerable<FeatureRecord> Features { get; set; }

        public IEnumerable<AttributeRecord> Attributes { get; set; }

        public IEnumerable<AttributeValueRecord> AttributeValues { get; set; }

        public IEnumerable<DataTypeRecord> DataTypes { get; set; }
    }
}
