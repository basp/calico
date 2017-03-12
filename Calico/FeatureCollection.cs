// <copyright file="FeatureCollection.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System.Collections.Generic;
    using System.Data;

    public class FeatureCollection : IFeatureCollection
    {
        private readonly IEnumerable<FeatureRecord> features;
        private readonly DataTable dataTable;

        public FeatureCollection(
            IEnumerable<FeatureRecord> features,
            DataTable dataTable)
        {
            this.features = features;
            this.dataTable = dataTable;
        }

        public IEnumerable<FeatureRecord> Features => this.features;

        public DataTable DataTable => this.dataTable;
    }
}
