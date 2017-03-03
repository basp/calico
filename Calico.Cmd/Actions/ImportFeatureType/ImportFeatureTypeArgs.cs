// <copyright file="ImportFeatureTypeArgs.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using PowerArgs;

    public class ImportFeatureTypeArgs
    {
        [ArgRequired]
        [ArgDescription(DefaultArgDescriptions.ClientId)]
        public int ClientId { get; set; }

        [ArgRequired]
        [ArgDescription(DefaultArgDescriptions.Shapefile)]
        public string PathToShapefile { get; set; }

        [ArgRequired]
        public string Name { get; set; }
    }
}
