// <copyright file="ImportPlotArgs.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using PowerArgs;

    public class ImportPlotArgs
    {
        [ArgRequired]
        [ArgPosition(1)]
        [ArgDescription(DefaultArgDescriptions.ClientId)]
        public int ClientId { get; set; }

        [ArgRequired]
        [ArgPosition(2)]
        [ArgDescription(DefaultArgDescriptions.FeatureTypeId)]
        public int FeatureTypeId { get; set; }

        [ArgRequired]
        [ArgPosition(3)]
        [ArgDescription(DefaultArgDescriptions.Shapefile)]
        public string PathToShapefile { get; set; }

        [ArgDefaultValue(4326)]
        [ArgDescription(DefaultArgDescriptions.SpatialReferenceSystem)]
        public int SRID { get; set; }

        [ArgDescription("The name for the plot")]
        public string Name { get; set; }
    }
}
