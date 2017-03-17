// <copyright file="MultiLineString.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Parsers.Wkt
{
    using System.Collections.Generic;

    public class MultiLineString : IGeometry
    {
        private IEnumerable<LineString> lineStrings;

        public MultiLineString(IEnumerable<LineString> lineStrings)
        {
            this.lineStrings = lineStrings;
        }

        public static string Ident => nameof(MultiLineString).ToUpperInvariant();

        public GeometryType Type => GeometryType.MultiLineString;

        public IEnumerable<Coordinate> Coordinates => new Coordinate[0];

        public IEnumerable<IGeometry> Geometries => this.lineStrings;
    }
}
