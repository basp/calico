// <copyright file="MultiPoint.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Parsers.Wkt
{
    using System;
    using System.Collections.Generic;

    public class MultiPoint : IGeometry
    {
        private readonly IEnumerable<Point> points;

        public MultiPoint(IEnumerable<Point> points)
        {
            this.points = points;
        }

        public static string Ident => nameof(MultiPoint).ToUpperInvariant();

        public GeometryType Type => GeometryType.MultiPoint;

        public IEnumerable<Coordinate> Coordinates => new Coordinate[0];

        public IEnumerable<IGeometry> Geometries => this.points;
    }
}
