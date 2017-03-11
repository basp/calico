// <copyright file="GetStylesArgs.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using PowerArgs;

    public class GetStylesArgs
    {
        [ArgRequired]
        public int FeatureTypeId { get; set; }
    }
}
