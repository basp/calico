// <copyright file="QuantifyDataSetArgs.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using PowerArgs;

    public class QuantifyDataSetArgs
    {
        [ArgRequired]
        public string PathToShapefile { get; set; }

        [ArgRequired]
        public string ColumnName { get; set; }
    }
}
