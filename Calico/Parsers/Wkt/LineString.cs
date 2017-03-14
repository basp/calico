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

        public IEnumerable<Coordinate> Coordinates { get; private set; }
    }
}
