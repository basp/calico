// <copyright file="GeometryType.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

#pragma warning disable SA1602 // Enumeration items must be documented
namespace Calico.Parsers.Wkt
{
    public enum GeometryType
    {
        None = 0,
        Point = 1,
        LineString = 2,
        Polygon = 3,
        MultiPoint = 4,
        MultiLineString = 5,
        MultiPolygon = 6,
    }
}
#pragma warning restore SA1602 // Enumeration items must be documented
