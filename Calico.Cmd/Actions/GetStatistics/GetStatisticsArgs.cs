// <copyright file="GetStatisticsArgs.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using PowerArgs;

    public class GetStatisticsArgs
    {
        [ArgRequired]
        [ArgPosition(1)]
        public string PathToShapefile { get; set; }
    }
}
