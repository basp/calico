// <copyright file="ShapefileFeatureCollection.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using DotSpatial.Data;

    public class ShapefileFeatureCollection : IFeatureCollection
    {
        private readonly Shapefile shapefile;
        private int srid;

        private ShapefileFeatureCollection(Shapefile shapefile, int srid)
        {
            this.shapefile = shapefile;
            this.srid = srid;
        }

        public static IFeatureCollection Create(string pathToShapefile, int srid = 4326)
        {
            var shapefile = Shapefile.OpenFile(pathToShapefile);
            return new ShapefileFeatureCollection(shapefile, srid);
        }

        public DataTable GetDataTable()
        {
            return this.shapefile.DataTable;
        }

        public IEnumerable<FeatureRecord> GetFeatures()
        {
            return this.shapefile.Features.Select(this.CreateRecord);
        }

        private FeatureRecord CreateRecord(IFeature feature, int index)
        {
            return new FeatureRecord
            {
                Index = index,
                Wkt = feature.Geometry.ToString(),
                SRID = this.srid,
            };
        }
    }
}
