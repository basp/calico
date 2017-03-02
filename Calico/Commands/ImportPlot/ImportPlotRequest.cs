// <copyright file="ImportPlotRequest.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    public class ImportPlotRequest
    {
        public int ClientId { get; set; }

        public int FeatureTypeId { get; set; }

        public string Name { get; set; }

        public string PathToShapefile { get; set; }

        public int SRID { get; set; }
    }
}
