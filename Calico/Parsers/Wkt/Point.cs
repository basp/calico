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

        public Coordinate Coordinate { get; private set; }
    }
}
