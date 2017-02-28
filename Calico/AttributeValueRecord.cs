// <copyright file="AttributeValueRecord.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    public class AttributeValueRecord
    {
        public int DataSetId { get; set; }

        public int AttributeIndex { get; set; }

        public int FeatureIndex { get; set; }

        public double? DoubleValue { get; set; }

        public long? LongValue { get; set; }

        public string StringValue { get; set; }
    }
}
