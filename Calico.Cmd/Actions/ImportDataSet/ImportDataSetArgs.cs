// <copyright file="ImportDataSetArgs.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using PowerArgs;

    public class ImportDataSetArgs
    {
        [ArgRequired]
        public int PlotId { get; set; }

        [ArgRequired]
        public int FeatureTypeId { get; set; }

        [ArgRequired]
        public string PathToShapefile { get; set; }

        public string Name { get; set; }
    }
}
