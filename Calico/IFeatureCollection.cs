// <copyright file="IFeatureCollection.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System.Collections.Generic;
    using System.Data;

    public interface IFeatureCollection
    {
        IEnumerable<FeatureRecord> GetFeatures();

        DataTable GetDataTable();
    }
}
