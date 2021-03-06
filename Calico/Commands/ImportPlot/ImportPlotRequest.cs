﻿// <copyright file="ImportPlotRequest.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;

    public class ImportPlotRequest
    {
        public int TenantId { get; set; }

        public int FeatureTypeId { get; set; }

        public string Name { get; set; }

        public DateTime DateCreated { get; set; }

        public int SRID { get; set; }
    }
}
