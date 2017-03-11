// <copyright file="GetClassesArgs.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using PowerArgs;

    public class GetClassesArgs
    {
        [ArgRequired]
        public string PathToShapefile { get; set; }

        [ArgRequired]
        public string ColumnName { get; set; }

        [ArgDefaultValue(false)]
        public bool Normalize { get; set; }

        [ArgDefaultValue(2)]
        public int Depth { get; set; }
    }
}
