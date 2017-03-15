// <copyright file="Point.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Parsers.Wkt
{
    using System.Collections.Generic;

    public class Point : IGeometry
    {
        private readonly Coordinate coordinate;

        public Point(Coordinate coordinate)
        {
            this.coordinate = coordinate;
        }

        public static string Ident => nameof(Point).ToUpperInvariant();

        public GeometryType Type => GeometryType.Point;

        public IEnumerable<Coordinate> Coordinates =>
            new[] { this.coordinate };

        public IEnumerable<IGeometry> Geometries =>
            new IGeometry[0];

        public override string ToString()
        {
            return $"{Ident} ({this.coordinate.Lon} {this.coordinate.Lat})";
        }
    }
}
