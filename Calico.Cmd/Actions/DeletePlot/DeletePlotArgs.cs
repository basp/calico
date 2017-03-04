// <copyright file="DeletePlotArgs.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using PowerArgs;

    public class DeletePlotArgs
    {
        [ArgRequired]
        [ArgPosition(1)]
        public int Id { get; set; }
    }
}
