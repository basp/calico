// <copyright file="IFeatureCollectionExtensions.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Calico.Parsers.Wkt;
    using Newtonsoft.Json;
    using Serilog;
    using Sprache;

    public static class IFeatureCollectionExtensions
    {
        public static string ToGeoJson(this IFeatureCollection self)
        {
            var features = self.Features
                .Select(x => CreateFeature(self, x));

            var collection = new
            {
                type = "FeatureCollection",
                features = features,
            };

            return JsonConvert.SerializeObject(collection);
        }

        private static Feature CreateFeature(
            IFeatureCollection features,
            FeatureRecord rec)
        {
            var row = features.DataTable.Rows[rec.Index];
            var cols = features.DataTable.Columns;

            var geom = CreateGeometry(rec);
            var props = row.Table.Columns
                .Cast<DataColumn>()
                .ToDictionary(x => x.ColumnName, x => row[x]);

            return new Feature(geom, props);
        }

        private static Geometry CreateGeometry(
            FeatureRecord rec)
        {
            var poly = Grammar.Polygon.Parse(rec.Wkt);
            var coords = poly.Lines
                .SelectMany(x => x.Select(y => new Coordinate(y.Lon, y.Lat)))
                .Select(x => new[] { x.Lon, x.Lat });

            return new Geometry(Polygon.Ident.Capitalize(), coords);
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

            [JsonProperty("type")]
            public string Type { get; private set; }

            [JsonProperty("geometry")]
            public Geometry Geometry { get; private set; }

            [JsonProperty("properties")]
            public IDictionary<string, object> Properties { get; private set; }
        }

        private class Geometry
        {
            public Geometry(string type, IEnumerable<double[]> coordinates)
            {
                this.Type = type;
                this.Coordinates = coordinates;
            }

            [JsonProperty("type")]
            public string Type { get; private set; }

            [JsonProperty("coordinates")]
            public IEnumerable<double[]> Coordinates { get; private set; }
        }
    }
}
