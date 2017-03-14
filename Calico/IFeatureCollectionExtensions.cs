// <copyright file="IFeatureCollectionExtensions.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Calico.Parsers.Wkt;
    using Sprache;

    public static class IFeatureCollectionExtensions
    {
        public static string ToGeoJson(this IFeatureCollection self)
        {
            var collection =
                self.Features.Select(x => CreateFeature(self, x));

            // TODO: Convert to JSON
            throw new NotImplementedException();
        }

        private static Feature CreateFeature(
            IFeatureCollection features,
            FeatureRecord rec)
        {
            var g = CreateGeometry(rec);
            var props = new Dictionary<string, object>();
            return new Feature(g, props);
        }

        private static Geometry CreateGeometry(
            FeatureRecord rec)
        {
            var poly = Grammar.Polygon.Parse(rec.Wkt);
            var coords = poly.Lines
                .SelectMany(x => x.SelectMany(y => new[] { y.Lon, y.Lat }))
                .ToArray();

            return new Geometry(Polygon.Ident, coords);
        }

        private class Feature
        {
            public Feature(
                Geometry geometry,
                IDictionary<string, object> properties)
            {
                this.Type = "Feature";
                this.Geometry = geometry;
                this.Properties = properties;
            }

            public string Type { get; private set; }

            public Geometry Geometry { get; private set; }

            public IDictionary<string, object> Properties { get; private set; }
        }

        private class Geometry
        {
            public Geometry(string type, double[] coordinates)
            {
                this.Type = type;
                this.Coordinates = coordinates;
            }

            public string Type { get; private set; }

            public double[] Coordinates { get; private set; }
        }
    }
}
