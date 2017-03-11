// <copyright file="ImportStyleArgs.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using PowerArgs;

    public class ImportStyleArgs
    {
        [ArgRequired]
        [ArgPosition(1)]
        [ArgDescription(DefaultArgDescriptions.Shapefile)]
        public string PathToShapefile { get; set; }

        [ArgRequired]
        [ArgPosition(2)]
        [ArgDescription("The name of the attribute from the data set in the shapefile")]
        public string ColumnName { get; set; }
    }
}
