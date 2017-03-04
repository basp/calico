// <copyright file="DeleteDataSetArgs.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using PowerArgs;

    public class DeleteDataSetArgs
    {
        [ArgRequired]
        [ArgPosition(1)]
        public int Id { get; set; }
    }
}
