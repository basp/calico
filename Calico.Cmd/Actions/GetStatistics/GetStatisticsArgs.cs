// <copyright file="GetStatisticsArgs.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using PowerArgs;

    public class GetStatisticsArgs
    {
        [ArgRequired]
        public string PathToShapefile { get; set; }
    }
}
