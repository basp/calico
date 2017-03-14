// <copyright file="Coordinate.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Parsers.Wkt
{
    public struct Coordinate
    {
        public Coordinate(double lon, double lat)
        {
            this.Lon = lon;
            this.Lat = lat;
        }

        public double Lon { get; private set; }

        public double Lat { get; private set; }
    }
}
