// <copyright file="ImportPlotArgs.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using PowerArgs;

    public class ImportPlotArgs
    {
        [ArgRequired]
        [ArgDescription(DefaultArgDescriptions.ClientId)]
        public int ClientId { get; set; }

        [ArgRequired]
        [ArgDescription(DefaultArgDescriptions.FeatureTypeId)]
        public int FeatureTypeId { get; set; }

        [ArgRequired]
        [ArgDescription(DefaultArgDescriptions.Shapefile)]
        public string PathToShapefile { get; set; }

        [ArgDefaultValue(4326)]
        [ArgDescription(DefaultArgDescriptions.SpatialReferenceSystem)]
        public int SRID { get; set; }

        public string Name { get; set; }
    }
}
