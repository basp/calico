// <copyright file="Grammar.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Parsers.Wkt
{
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

        private const char LPAREN = '(';
        private const char RPAREN = ')';
        private const char COMMA = ',';

        private static readonly Parser<char> Comma = Parse.Char(COMMA).Token();

        private static readonly Parser<char> Lparen = Parse.Char(LPAREN).Token();

        private static readonly Parser<char> Rparen = Parse.Char(RPAREN).Token();

        public static Parser<Point> Point() => Point(Lparen, Rparen);

        public static Parser<Point> Point(Parser<string> ident) =>
            Point(ident, Lparen, Rparen);

        public static Parser<Point> Point(
            Parser<char> lparen,
            Parser<char> rparen) =>
            Point(Parse.String(Wkt.Point.Ident).Text(), lparen, rparen);

        public static Parser<Point> Point(
            Parser<string> ident,
            Parser<char> lparen,
            Parser<char> rparen) =>
            from id in ident.Token()
            from lp in lparen
            from coord in Coordinate
            from rp in rparen
            select new Point(coord);

        public static Parser<MultiPoint> MultiPoint()
        {
            var @explicit = MultiPoint(
                Parse.String(Wkt.MultiPoint.Ident).Text(),
                Point(Parse.Return(Wkt.Point.Ident)));

            var @implicit = MultiPoint(
                Parse.String(Wkt.MultiPoint.Ident).Text(),
                Point(
                    Parse.Return(Wkt.Point.Ident).Text(),
                    Parse.Return(LPAREN),
                    Parse.Return(RPAREN)));

            return @explicit.Or(@implicit);
        }

        public static Parser<MultiPoint> MultiPoint(Parser<string> ident, Parser<Point> point) =>
            from id in ident.Token()
            from lp in Lparen
            from points in point.DelimitedBy(Comma).Token()
            from rp in Rparen
            select new MultiPoint(points);

        public static Parser<LineString> LineString() =>
            LineString(Parse.String(Wkt.LineString.Ident).Text());

        public static Parser<LineString> LineString(Parser<string> ident) =>
            from id in ident.Token()
            from lparen in Lparen
            from coords in Coordinate.DelimitedBy(Comma)
            from rparen in Rparen
            select new LineString(coords);

        public static Parser<Polygon> Polygon() =>
            Polygon(Parse.String(Wkt.Polygon.Ident).Text());

        public static Parser<Polygon> Polygon(Parser<string> ident) =>
            from id in ident.Token()
            from lp in Lparen
            from lineStrings in LineString(Parse.Return(Wkt.LineString.Ident)).DelimitedBy(Comma)
            from rp in Rparen
            select new Polygon(lineStrings);

        public static Parser<MultiPolygon> MultiPolygon() =>
            MultiPolygon(Parse.String(Wkt.MultiPolygon.Ident).Text());

        public static Parser<MultiPolygon> MultiPolygon(Parser<string> ident) =>
            from id in ident.Token()
            from lp in Lparen
            from polygons in Polygon(Parse.Return(Wkt.Polygon.Ident)).DelimitedBy(Comma)
            from rp in Rparen
            select new MultiPolygon(polygons);

        public static Parser<IGeometry> Geometry() => Point()
            .Or<IGeometry>(MultiPoint()) // Type hint is only needed once
            .Or(LineString())
            .Or(Polygon())
            .Or(MultiPolygon());
    }
}