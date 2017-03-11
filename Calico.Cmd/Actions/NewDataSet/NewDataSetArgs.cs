// <copyright file="NewDataSetArgs.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using PowerArgs;

    public class NewDataSetArgs
    {
        [ArgRequired]
        [ArgPosition(1)]
        [ArgDescription(DefaultArgDescriptions.PlotId)]
        public int PlotId { get; set; }

        [ArgRequired]
        [ArgPosition(2)]
        [ArgDescription(DefaultArgDescriptions.FeatureTypeId)]
        public int FeatureTypeId { get; set; }

        [ArgRequired]
        [ArgPosition(3)]
        [ArgDescription("The name of the data set")]
        public string Name { get; set; }
    }
}
