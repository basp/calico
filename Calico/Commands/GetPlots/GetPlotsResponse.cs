// <copyright file="GetPlotsResponse.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System.Collections.Generic;

    public class GetPlotsResponse
    {
        public IEnumerable<PlotRecord> Plots { get; set; }
    }
}
