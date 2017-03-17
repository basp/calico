// <copyright file="GraphQLArgs.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using PowerArgs;

    public class GraphQLArgs
    {
        [ArgRequired]
        [ArgPosition(1)]
        public string Query { get; set; }
    }
}