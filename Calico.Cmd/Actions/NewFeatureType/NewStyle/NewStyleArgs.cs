// <copyright file="NewStyleArgs.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using PowerArgs;

    public class NewStyleArgs
    {
        [ArgRequired]
        [ArgPosition(1)]
        [ArgDescription(DefaultArgDescriptions.FeatureTypeId)]
        public int FeatureTypeId { get; set; }

        [ArgRequired]
        [ArgPosition(2)]
        [ArgDescription("The index of the attribute from the feature type")]
        public int AttributeIndex { get; set; }

        [ArgRequired]
        [ArgPosition(3)]
        [ArgDescription("The type of style to use")]
        public int StyleTypeId { get; set; }

        [ArgRequired]
        [ArgPosition(4)]
        [ArgDescription("The name of the style")]
        public string Name { get; set; }
    }
}
