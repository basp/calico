// <copyright file="ImportFeatureTypeRequest.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    public class ImportFeatureTypeRequest
    {
        public int ClientId { get; set; }

        public string PathToShapefile { get; set; }

        public string Name { get; set; }
    }
}
