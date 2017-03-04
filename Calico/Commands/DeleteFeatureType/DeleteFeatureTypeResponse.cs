// <copyright file="DeleteFeatureTypeResponse.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    public class DeleteFeatureTypeResponse
    {
        public FeatureTypeRecord FeatureType { get; set; }

        public int RowCount { get; set; }
    }
}
