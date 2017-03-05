// <copyright file="ImportDataSetArgs.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using PowerArgs;

    public class ImportDataSetArgs
    {
        [ArgRequired]
        [ArgDescription(DefaultArgDescriptions.PlotId)]
        public int PlotId { get; set; }

        [ArgRequired]
        [ArgDescription(DefaultArgDescriptions.FeatureTypeId)]
        public int FeatureTypeId { get; set; }

        [ArgRequired]
        [ArgDescription(DefaultArgDescriptions.Shapefile)]
        public string PathToShapefile { get; set; }

        [ArgDescription("The name of the of the data set")]
        public string Name { get; set; }

        [ArgDefaultValue(4326)]
        [ArgDescription(DefaultArgDescriptions.SpatialReferenceSystem)]
        public int SRID { get; set; }
    }
}
