﻿// <copyright file="NewFeatureTypeArgs.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using PowerArgs;

    public class NewFeatureTypeArgs
    {
        [ArgRequired]
        [ArgDescription(DefaultArgDescriptions.ClientId)]
        public int ClientId { get; set; }

        [ArgRequired]
        [ArgDescription("The name of the feature type")]
        public string Name { get; set; }
    }
}
