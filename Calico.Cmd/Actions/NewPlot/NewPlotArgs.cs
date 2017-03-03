// <copyright file="NewPlotArgs.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using PowerArgs;

    public class NewPlotArgs
    {
        [ArgRequired]
        [ArgDescription(DefaultArgDescriptions.PlotId)]
        public int ClientId { get; set; }

        [ArgRequired]
        [ArgDescription(DefaultArgDescriptions.Shapefile)]
        public string PathToShapefile { get; set; }

        [ArgRequired]
        [ArgDescription("The name of the plot")]
        public string Name { get; set; }

        [ArgDefaultValue(4326)]
        [ArgDescription(DefaultArgDescriptions.SpatialReferenceSystem)]
        public int SRID { get; set; }
    }
}
