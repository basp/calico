// <copyright file="GetClassesArgs.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using PowerArgs;

    public class GetClassesArgs
    {
        [ArgRequired]
        [ArgPosition(1)]
        public string PathToShapefile { get; set; }

        [ArgRequired]
        [ArgPosition(2)]
        public string ColumnName { get; set; }

        [ArgDefaultValue(false)]
        public bool Normalize { get; set; }

        [ArgDefaultValue(2)]
        public int Depth { get; set; }
    }
}
