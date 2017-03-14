// <copyright file="Grammar.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

#pragma warning disable SA1401 // Fields must be private
namespace Calico.Parsers.Wkt
{
    using System.Collections.Generic;
    using Sprache;

    public static class Grammar
    {
        public static Parser<double> Double =
            from s in Parse.Decimal.Token().Text()
            select double.Parse(s);

        public static Parser<Coordinate> Coordinate =
            from lon in Double.Token()
            from lat in Double.Token()
            select new Coordinate(lon, lat);

        public static Parser<LineString> LineString =
            from id in Parse.String("LINESTRING").Token()
            from coords in LineStringCoordinates
            select new LineString(coords);

        public static Parser<IEnumerable<Coordinate>> LineStringCoordinates =
            from lparen in Parse.Char('(').Token()
            from coords in Coordinate.DelimitedBy(Parse.Char(',').Token())
            from rparen in Parse.Char(')').Token()
            select coords;

        public static Parser<Point> Point =
            from id in Parse.String("POINT").Token()
            from lparen in Parse.Char('(').Token()
            from coord in Coordinate
            from rparen in Parse.Char(')').Token()
            select new Point(coord);

        public static Parser<Polygon> Polygon =>
            from id in Parse.String("POLYGON").Token()
            from lparen in Parse.Char('(').Token()
            from lineStrings in LineStringCoordinates.AtLeastOnce()
            from rparen in Parse.Char(')').Token()
            select new Polygon(lineStrings);
    }
}
#pragma warning restore SA1401 // Fields must be private