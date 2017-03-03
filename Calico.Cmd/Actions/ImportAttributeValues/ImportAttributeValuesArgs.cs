// <copyright file="ImportAttributeValuesArgs.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using PowerArgs;

    public class ImportAttributeValuesArgs
    {
        [ArgRequired]
        [ArgDescription(DefaultArgDescriptions.DataSetId)]
        public int DataSetId { get; set; }

        [ArgRequired]
        [ArgDescription(DefaultArgDescriptions.Shapefile)]
        public string PathToShapefile { get; set; }
    }
}
