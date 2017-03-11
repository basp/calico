// <copyright file="NewPlotArgs.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using PowerArgs;

    public class NewPlotArgs
    {
        [ArgRequired]
        [ArgPosition(1)]
        [ArgDescription(DefaultArgDescriptions.PlotId)]
        public int ClientId { get; set; }

        [ArgRequired]
        [ArgPosition(2)]
        [ArgDescription(DefaultArgDescriptions.Shapefile)]
        public string PathToShapefile { get; set; }

        [ArgRequired]
        [ArgPosition(3)]
        [ArgDescription("The name of the plot")]
        public string Name { get; set; }

        [ArgDefaultValue(4326)]
        [ArgPosition(4)]
        [ArgDescription(DefaultArgDescriptions.SpatialReferenceSystem)]
        public int SRID { get; set; }
    }
}
