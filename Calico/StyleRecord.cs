// <copyright file="StyleRecord.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    public class StyleRecord
    {
        public int Id { get; set; }

        public int FeatureTypeId { get; set; }

        public int AttributeIndex { get; set; }

        public int StyleTypeId { get; set; }

        public string Name { get; set; }
    }
}
