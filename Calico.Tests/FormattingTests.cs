// <copyright file="FormattingTests.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FormattingTests
    {
        [TestMethod]
        public void CapitlizeText()
        {
            var txt = @"HUMAN";
            var res = txt.Capitalize();
            Assert.AreEqual("Human", res);
        }
    }
}
