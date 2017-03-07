// <copyright file="Normal.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Classification
{
    using System;

    // https://en.wikipedia.org/wiki/Normal_distribution
    public static class Normal
    {
        public static double Standard(double x)
        {
            x = Math.Abs(x);
            var u = Math.Pow(Math.E, -(0.5 * Math.Pow(x, 2)));
            var w = Math.Sqrt(2 * Math.PI);
            return u / w;
        }

        public static double StandardGauss(double x)
        {
            x = Math.Abs(x);
            var u = Math.Pow(Math.E, -Math.Pow(x, 2));
            var w = Math.Sqrt(Math.PI);
            return u / w;
        }

        public static double Stigler(double x)
        {
            // var x = Math.Abs(x);
        }
    }
}
