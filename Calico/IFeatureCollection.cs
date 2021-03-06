﻿// <copyright file="IFeatureCollection.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System.Collections.Generic;
    using System.Data;
    using System.IO;

    public interface IFeatureCollection
    {
        IEnumerable<FeatureRecord> Features { get; }

        DataTable DataTable { get; }
    }
}
