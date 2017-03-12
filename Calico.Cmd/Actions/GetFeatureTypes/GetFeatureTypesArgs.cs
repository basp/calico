// <copyright file="GetFeatureTypesArgs.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using PowerArgs;

    public class GetFeatureTypesArgs
    {
        [ArgRequired]
        [ArgPosition(1)]
        [ArgDescription(DefaultArgDescriptions.ClientId)]
        public int ClientId { get; set; }

        [ArgDefaultValue(50)]
        [ArgPosition(2)]
        [ArgDescription(DefaultArgDescriptions.Top)]
        public int Top { get; set; }
    }
}
