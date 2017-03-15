// <copyright file="SandboxArgs.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using PowerArgs;

    public class SandboxArgs
    {
        [ArgRequired]
        [ArgPosition(1)]
        public int DataSetId { get; set; }
    }
}