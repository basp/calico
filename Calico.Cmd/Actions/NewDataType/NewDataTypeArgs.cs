// <copyright file="NewDataTypeArgs.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using PowerArgs;

    public class NewDataTypeArgs
    {
        [ArgRequired]
        [ArgDescription("The display name of the data type")]
        public string Name { get; set; }

        [ArgRequired]
        [ArgDescription("The SQL Server type name")]
        public string SqlType { get; set; }

        [ArgRequired]
        [ArgDescription("The BCL (.NET) type name")]
        public string BclType { get; set; }
    }
}
