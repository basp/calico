﻿// <copyright file="GetDataSetsArgs.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using PowerArgs;

    public class GetDataSetsArgs
    {
        [ArgRequired]
        [ArgPosition(1)]
        [ArgDescription(DefaultArgDescriptions.PlotId)]
        public int PlotId { get; set; }

        [ArgDefaultValue(50)]
        [ArgPosition(2)]
        [ArgDescription(DefaultArgDescriptions.Top)]
        public int Top { get; set; }
    }
}
