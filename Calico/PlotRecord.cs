// <copyright file="PlotRecord.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    public class PlotRecord
    {
        public int Id { get; set; }

        public int TenantId { get; set; }

        public string Name { get; set; }

        public string Wkt { get; set; }

        public int SRID { get; set; }
    }
}
