// <copyright file="NewClientArgs.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using PowerArgs;

    public class NewClientArgs
    {
        [ArgRequired]
        [ArgDescription("The name of the new client")]
        public string Name { get; set; }
    }
}
