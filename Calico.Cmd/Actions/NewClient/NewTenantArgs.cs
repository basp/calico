// <copyright file="NewTenantArgs.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using PowerArgs;

    public class NewTenantArgs
    {
        [ArgRequired]
        [ArgPosition(1)]
        [ArgDescription("The name of the new tenant")]
        public string Name { get; set; }
    }
}
