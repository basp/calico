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
            var geometry = Grammar.Geometry().Parse(rec.Wkt);
            return Geometry.Create(geometry);
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
            public string Type { get; }

            [JsonProperty("geometry")]
            public Geometry Geometry { get; }

            [JsonProperty("properties")]
            public IDictionary<string, object> Properties { get; }
        }

        private class Geometry
        {
            private readonly GeometryType type;
            private readonly object coordinates;

            private Geometry(GeometryType type, object coordinates)
            {
                this.type = type;
                this.coordinates = coordinates;
            }

            [JsonProperty("type")]
            public string Type => this.type.ToString();

            [JsonProperty("coordinates")]
            public object Coordinates => this.coordinates;

            // NOTE: 
            // This is bascially a parser so the switch is
            // justified (somewhat... not really)
            public static Geometry Create(IGeometry geometry)
            {
                object coords;
                switch (geometry.Type)
                {
                    case GeometryType.Point:
                        coords = GetCoordinates((Point)geometry);
                        return new Geometry(GeometryType.Point, coords);
                    case GeometryType.Polygon:
                        coords = GetCoordinates((Polygon)geometry);
                        return new Geometry(GeometryType.Polygon, coords);
                    case GeometryType.MultiPolygon:
                        coords = GetCoordinates((MultiPolygon)geometry);
                        return new Geometry(GeometryType.Polygon, coords);
                    default:
                        throw new NotImplementedException();
                }
            }

            private static IEnumerable<double> GetCoordinates(
                Coordinate coord)
            {
                return new[] { coord.Lon, coord.Lat };
            }

            private static IEnumerable<double> GetCoordinates(
                Point point)
            {
                var c = point.Coordinates.First();
                return GetCoordinates(c);
            }

            private static IEnumerable<IEnumerable<double>> GetCoordinates(
                LineString lineString)
            {
                return lineString.Coordinates
                    .Select(GetCoordinates);
            }

            private static IEnumerable<IEnumerable<IEnumerable<double>>> GetCoordinates(
                Polygon polygon)
            {
                return polygon.Geometries
                    .Select(x => GetCoordinates((LineString)x));
            }

            private static IEnumerable<IEnumerable<IEnumerable<IEnumerable<double>>>> GetCoordinates(
                MultiPolygon multiPolygon)
            {
                return multiPolygon.Geometries
                    .Select(x => GetCoordinates((Polygon)x));
            }
        }
    }
}