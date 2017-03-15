// <copyright file="Grammar.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Parsers.Wkt
{
    using System.Collections.Generic;
    using System.Linq;
    using Sprache;

    public static class Grammar
    {
        public static readonly Parser<double> Double =
            from s in Parse.Decimal.Token().Text()
            select double.Parse(s);

        public static readonly Parser<Coordinate> Coordinate =
            from lon in Double.Token()
            from lat in Double.Token()
            select new Coordinate(lon, lat);

        public static readonly Parser<IEnumerable<Coordinate>> Coordinates =
            from lparen in Parse.Char(LPAREN).Token()
            from coords in Coordinate.DelimitedBy(Parse.Char(COMMA).Token())
            from rparen in Parse.Char(RPAREN).Token()
            select coords;

        public static readonly Parser<LineString> LineString =
            from id in Parse.String(Wkt.LineString.Ident).Token()
            from coords in Coordinates.AtLeastOnce()
            select new LineString(coords.SelectMany(x => x));

        public static readonly Parser<Point> Point =
            from id in Parse.String(Wkt.Point.Ident).Token()
            from lparen in Parse.Char(LPAREN).Token()
            from coord in Coordinate
            from rparen in Parse.Char(RPAREN).Token()
            select new Point(coord);

        public static readonly Parser<Polygon> Polygon =
            from id in Parse.String(Wkt.Polygon.Ident).Token()
            from lparen in Parse.Char(LPAREN).Token()
            from lineStrings in Coordinates.AtLeastOnce()
            from rparen in Parse.Char(RPAREN).Token()
            select new Polygon(lineStrings);

        public static readonly Parser<MultiPolygon> MultiPolygon =
            from id in Parse.String(Wkt.MultiPolygon.Ident).Token()
            from lparen in Parse.Char(LPAREN).Token()
            from stuff in Coordinates.AtLeastOnce()
            from rparen in Parse.Char(RPAREN).Token()
            let polygons = new List<Polygon>()
            select new MultiPolygon(polygons);

        private const char LPAREN = '(';
        private const char RPAREN = ')';
        private const char COMMA = ',';
    }
}