// <copyright file="DataSetRecord.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;

    public class DataSetRecord
    {
        public int Id { get; set; }

        public int PlotId { get; set; }

        public int FeatureTypeId { get; set; }

        public string Name { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
