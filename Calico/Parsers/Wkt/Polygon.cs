// <copyright file="Polygon.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Parsers.Wkt
{
    using System.Collections.Generic;

    public class Polygon : IGeometry
    {
        private readonly IEnumerable<LineString> lineStrings;

        public Polygon(IEnumerable<LineString> lineStrings)
        {
            this.lineStrings = lineStrings;
        }

        public static string Ident => nameof(Polygon).ToUpperInvariant();

        public GeometryType Type => GeometryType.Polygon;

        public IEnumerable<Coordinate> Coordinates => new Coordinate[0];

        public IEnumerable<IGeometry> Geometries => this.lineStrings;
    }
}
