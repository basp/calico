// <copyright file="NewStyleRequest.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    public class NewStyleRequest
    {
        public int FeatureTypeId { get; set; }

        public int AttributeIndex { get; set; }

        public int StyleTypeId { get; set; }

        public string Name { get; set; }
    }
}
