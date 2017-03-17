// <copyright file="WktParserTests.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Tests
{
    using System.Linq;
    using Calico.Parsers.Wkt;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Sprache;

    [TestClass]
    public class WktParserTests
    {
        [TestMethod]
        public void ParseCoordinate()
        {
            var wkt = "10 30";
            var res = Grammar.Coordinate.Parse(wkt);

            Assert.AreEqual(10.00, res.Lon);
            Assert.AreEqual(30.00, res.Lat);
        }

        [TestMethod]
        public void ParsePoint()
        {
            var wkt = "POINT (10 30)";
            var res = Grammar.Point().Parse(wkt);

            Assert.AreEqual(10.00, res.Coordinates.First().Lon);
            Assert.AreEqual(30.00, res.Coordinates.First().Lat);
        }

        [TestMethod]
        public void ParseLineString()
        {
            var wkt = "LINESTRING (30 10, 10 30, 40 40)";
            var res = Grammar.LineString().Parse(wkt);

            var c1 = res.Coordinates.First();
            var c2 = res.Coordinates.Skip(1).First();

            Assert.AreEqual(30.0, c1.Lon);
            Assert.AreEqual(10.0, c1.Lat);

            Assert.AreEqual(10.0, c2.Lon);
            Assert.AreEqual(30.0, c2.Lat);
        }

        [TestMethod]
        public void ParsePolygon()
        {
            var wkt = "POLYGON ((5.8 51.2, 5.3 51.4, 5.1 51.4))";
            var res = Grammar.Polygon().Parse(wkt);
            var coords = res.Geometries.First().Coordinates.ToArray();

            Assert.AreEqual(5.8, coords[0].Lon);
            Assert.AreEqual(51.2, coords[0].Lat);

            Assert.AreEqual(5.3, coords[1].Lon);
            Assert.AreEqual(51.4, coords[1].Lat);

            Assert.AreEqual(5.1, coords[2].Lon);
            Assert.AreEqual(51.4, coords[2].Lat);
        }

        [TestMethod]
        public void ParseImplicitMultiPoint()
        {
            var wkt = "MULTIPOINT (10 40, 40 30, 20 20, 30 10)";
            var res = Grammar.MultiPoint().Parse(wkt);

            var p1 = res.Geometries.First();
            var c1 = p1.Coordinates.First();

            var p2 = res.Geometries.Skip(1).First();
            var c2 = p2.Coordinates.First();

            Assert.AreEqual(10, c1.Lon);
            Assert.AreEqual(40, c1.Lat);

            Assert.AreEqual(40, c2.Lon);
            Assert.AreEqual(30, c2.Lat);
        }

        [TestMethod]
        public void ParseExplicitMultiPoint()
        {
            var wkt = "MULTIPOINT ((10 40), (40 30), (20 20), (30 10))";
            var res = Grammar.MultiPoint().Parse(wkt);

            var p1 = res.Geometries.First();
            var c1 = p1.Coordinates.First();

            var p2 = res.Geometries.Skip(1).First();
            var c2 = p2.Coordinates.First();

            Assert.AreEqual(10.0, c1.Lon);
            Assert.AreEqual(40.0, c1.Lat);
        }

        [TestMethod]
        public void ParseMultiPolygon()
        {
            var wkt = "MULTIPOLYGON (((30 20, 45 40, 10 40, 30 20)), ((15 5, 40 10, 10 20, 5 10, 15 5)))";
            var res = Grammar.MultiPolygon().Parse(wkt);
            var poly1 = res.Geometries.First().Geometries.First().Coordinates.ToArray();
            var poly2 = res.Geometries.First().Geometries.First().Coordinates.ToArray();

            Assert.AreEqual(30, poly1[0].Lon);
            Assert.AreEqual(20, poly1[0].Lat);
        }
    }
}
