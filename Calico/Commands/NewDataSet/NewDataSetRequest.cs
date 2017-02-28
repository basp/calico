// <copyright file="NewDataSetRequest.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;

    public class NewDataSetRequest
    {
        public int PlotId { get; set; }

        public int FeatureTypeId { get; set; }

        public string Name { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
