// <copyright file="ScanShapefileRequest.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    public class ScanShapefileRequest
    {
        public int TenantId { get; set; }

        public string PathToShapefile { get; set; }

        public int SRID { get; set; }
    }
}
