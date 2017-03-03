// <copyright file="ImportPlotArgs.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using PowerArgs;

    public class ImportPlotArgs
    {
        [ArgRequired]
        public int ClientId { get; set; }

        [ArgRequired]
        public int FeatureTypeId { get; set; }

        [ArgRequired]
        public string PathToShapefile { get; set; }

        [ArgDefaultValue(4326)]
        public int SRID { get; set; }

        public string Name { get; set; }
    }
}
