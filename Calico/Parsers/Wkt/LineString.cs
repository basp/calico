// <copyright file="LineString.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Parsers.Wkt
{
    using System.Collections.Generic;

    public class LineString
    {
        public LineString(IEnumerable<Coordinate> coordinates)
        {
            this.Coordinates = coordinates;
        }

        public static string Ident => nameof(LineString).ToUpperInvariant();

        public IEnumerable<Coordinate> Coordinates { get; private set; }
    }
}
