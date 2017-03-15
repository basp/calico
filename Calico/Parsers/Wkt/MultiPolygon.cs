// <copyright file="MultiPolygon.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Parsers.Wkt
{
    using System;
    using System.Collections.Generic;

    public class MultiPolygon : IGeometry
    {
        private readonly IEnumerable<Polygon> polygons;

        public MultiPolygon(IEnumerable<Polygon> polygons)
        {
            this.polygons = polygons;
        }

        public static string Ident => nameof(MultiPolygon).ToUpperInvariant();

        public GeometryType Type => GeometryType.MultiPolygon;

        public IEnumerable<Coordinate> Coordinates => new Coordinate[0];

        public IEnumerable<IGeometry> Geometries => this.polygons;
    }
}
