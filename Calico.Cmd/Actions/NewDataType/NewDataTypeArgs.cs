// <copyright file="NewDataTypeArgs.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using PowerArgs;

    public class NewDataTypeArgs
    {
        [ArgRequired]
        [ArgPosition(1)]
        [ArgDescription("The display name of the data type")]
        public string Name { get; set; }

        [ArgRequired]
        [ArgPosition(2)]
        [ArgDescription("The SQL Server type name")]
        public string SqlType { get; set; }

        [ArgRequired]
        [ArgPosition(3)]
        [ArgDescription("The BCL (.NET) type name")]
        public string BclType { get; set; }
    }
}
