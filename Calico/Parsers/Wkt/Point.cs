// <copyright file="Point.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Parsers.Wkt
{
    public class Point
    {
        public Point(Coordinate coordinate)
        {
            this.Coordinate = coordinate;
        }

        public static string Ident => nameof(Point).ToUpperInvariant();

        public Coordinate Coordinate { get; private set; }

        public override string ToString()
        {
            return $"{Ident} ({this.Coordinate.Lon} {this.Coordinate.Lat})";
        }
    }
}
