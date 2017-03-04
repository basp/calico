// <copyright file="DeleteFeatureTypeArgs.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using PowerArgs;

    public class DeleteFeatureTypeArgs
    {
        [ArgRequired]
        [ArgPosition(1)]
        [ArgDescription("The id of the feature type to delete")]
        public int Id { get; set; }
    }
}
