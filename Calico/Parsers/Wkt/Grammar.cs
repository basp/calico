// <copyright file="Grammar.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Parsers.Wkt
{
    using System.Collections.Generic;
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

        public static readonly Parser<LineString> LineString =
            from id in Parse.String(Wkt.LineString.Ident).Token()
            from coords in LineStringCoordinates
            select new LineString(coords);

        public static readonly Parser<IEnumerable<Coordinate>> LineStringCoordinates =
            from lparen in Parse.Char('(').Token()
            from coords in Coordinate.DelimitedBy(Parse.Char(',').Token())
            from rparen in Parse.Char(')').Token()
            select coords;

        public static readonly Parser<Point> Point =
            from id in Parse.String(Wkt.Point.Ident).Token()
            from lparen in Parse.Char('(').Token()
            from coord in Coordinate
            from rparen in Parse.Char(')').Token()
            select new Point(coord);

        public static readonly Parser<Polygon> Polygon =
            from id in Parse.String(Wkt.Polygon.Ident).Token()
            from lparen in Parse.Char('(').Token()
            from lineStrings in LineStringCoordinates.AtLeastOnce()
            from rparen in Parse.Char(')').Token()
            select new Polygon(lineStrings);
    }
}