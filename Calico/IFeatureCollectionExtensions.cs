// <copyright file="IFeatureCollectionExtensions.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Sprache;

    public static class IFeatureCollectionExtensions
    {
        public static string ToGeoJson(this IFeatureCollection self)
        {
            var collection = self.Features.Select(x => CreateFeature(self, x));
            throw new NotImplementedException();
        }

        private static Feature CreateFeature(
            IFeatureCollection features,
            FeatureRecord rec)
        {
            throw new NotImplementedException();
        }

        private static Geometry CreateGeometry(
            FeatureRecord rec)
        {
            var poly = Parsers.Wkt.Grammar.Polygon.Parse(rec.Wkt);
            throw new NotImplementedException();
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
