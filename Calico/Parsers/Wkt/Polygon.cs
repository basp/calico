// <copyright file="Polygon.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Parsers.Wkt
{
    using System.Collections.Generic;

    public class Polygon
    {
        public Polygon(IEnumerable<IEnumerable<Coordinate>> lines)
        {
            this.Lines = lines;
        }

        public IEnumerable<IEnumerable<Coordinate>> Lines { get; private set; }
    }
}
