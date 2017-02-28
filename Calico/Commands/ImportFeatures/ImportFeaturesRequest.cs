// <copyright file="ImportFeaturesRequest.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    public class ImportFeaturesRequest
    {
        public int DataSetId { get; set; }

        public string PathToShapefile { get; set; }

        public int SRID { get; set; }
    }
}
