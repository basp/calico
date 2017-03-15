// <copyright file="LineString.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Parsers.Wkt
{
    using System.Collections.Generic;

    public class LineString : IGeometry
    {
        private readonly IEnumerable<Coordinate> coordinates;

        public LineString(IEnumerable<Coordinate> coordinates)
        {
            this.coordinates = coordinates;
        }

        public static string Ident => nameof(LineString).ToUpperInvariant();

        public IEnumerable<Coordinate> Coordinates => this.coordinates;

        public GeometryType Type => GeometryType.LineString;

        public IEnumerable<IGeometry> Geometries => new IGeometry[0];
    }
}
