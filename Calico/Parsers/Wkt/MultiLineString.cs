// <copyright file="MultiLineString.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Parsers.Wkt
{
    using System;
    using System.Collections.Generic;

    public class MultiLineString : IGeometry
    {
        private IEnumerable<LineString> lineStrings;

        public MultiLineString(IEnumerable<LineString> lineStrings)
        {
            this.lineStrings = lineStrings;
        }

        public static string Ident => 
            nameof(MultiLineString).ToUpperInvariant();

        public GeometryType Type => 
            throw new NotImplementedException();

        public IEnumerable<Coordinate> Coordinates => 
            throw new NotImplementedException();

        public IEnumerable<IGeometry> Geometries => 
            throw new NotImplementedException();
    }
}
