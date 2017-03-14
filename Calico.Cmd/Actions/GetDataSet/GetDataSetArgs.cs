// <copyright file="GetDataSetArgs.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using PowerArgs;

    public class GetDataSetArgs
    {
        [ArgRequired]
        [ArgPosition(1)]
        public int DataSetId { get; set; }
    }
}
