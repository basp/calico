// <copyright file="GetFeatureTypesResponse.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System.Collections.Generic;

    public class GetFeatureTypesResponse
    {
        public IEnumerable<FeatureTypeRecord> FeatureTypes { get; set; }
    }
}
