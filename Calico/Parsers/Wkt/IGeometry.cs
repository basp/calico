// <copyright file="IGeometry.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Parsers.Wkt
{
    using System.Collections.Generic;

    public interface IGeometry
    {
        GeometryType Type { get; }

        IEnumerable<Coordinate> Coordinates { get; }

        IEnumerable<IGeometry> Geometries { get; }
    }
}
