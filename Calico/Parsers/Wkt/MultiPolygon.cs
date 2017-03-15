// <copyright file="MultiPolygon.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Parsers.Wkt
{
    using System.Collections.Generic;

    public class MultiPolygon
    {
        public MultiPolygon(IEnumerable<Polygon> polygons)
        {
            this.Polygons = polygons;
        }

        public static string Ident => nameof(MultiPolygon).ToUpperInvariant();

        public IEnumerable<Polygon> Polygons { get; private set; }
    }
}
