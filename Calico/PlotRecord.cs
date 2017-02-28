// <copyright file="PlotRecord.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using Microsoft.SqlServer.Types;

    public class PlotRecord
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public string Name { get; set; }

        public SqlGeometry Geometry { get; set; }
    }
}
