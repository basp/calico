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
            var res = Grammar.Point.Parse(wkt);

            Assert.AreEqual(10.00, res.Coordinate.Lon);
            Assert.AreEqual(30.00, res.Coordinate.Lat);
        }

        [TestMethod]
        public void ParsePolygonWithoutHoles()
        {
            var wkt = "POLYGON ((5.8 51.2, 5.3 51.4, 5.1 51.4))";
            var res = Grammar.Polygon.Parse(wkt);
            var coords = res.Lines.First().ToArray();

            Assert.AreEqual(5.8, coords[0].Lon);
            Assert.AreEqual(51.2, coords[0].Lat);

            Assert.AreEqual(5.3, coords[1].Lon);
            Assert.AreEqual(51.4, coords[1].Lat);

            Assert.AreEqual(5.1, coords[2].Lon);
            Assert.AreEqual(51.4, coords[2].Lat);
        }
    }
}
