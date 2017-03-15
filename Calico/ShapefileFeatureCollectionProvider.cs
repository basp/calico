// <copyright file="ShapefileFeatureCollectionProvider.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using System.Data;
    using System.Linq;
    using DotSpatial.Data;
    using Optional;

    using static Optional.Option;

    // TODO: This looks a `ICommand` why is it not a command?
    public class ShapefileFeatureCollectionProvider
    {
        private readonly string pathToShapefile;
        private readonly int srid;

        public ShapefileFeatureCollectionProvider(string pathToShapefile, int srid = 4326)
        {
            this.pathToShapefile = pathToShapefile;
            this.srid = srid;
        }

        public Option<IFeatureCollection, Exception> Get()
        {
            try
            {
                var shapefile = Shapefile.OpenFile(this.pathToShapefile);
                var features = shapefile.Features.Select(this.CreateRecord);
                var res = new FeatureCollection(features, shapefile.DataTable);
                return Some<IFeatureCollection, Exception>(res);
            }
            catch (Exception ex)
            {
                return None<IFeatureCollection, Exception>(ex);
            }
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
