// <copyright file="Chauvenet.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Classification
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Implements Chauvenet's criterion for identifying outliers in a sample.
    /// </summary>
    public static class Chauvenet
    {
        public static Func<double, double> Create(int sampleSize, double mean, double standardDeviation)
        {
            var func = new Func<double, double>(x =>
            {
                // https://en.wikipedia.org/wiki/Chauvenet%27s_criterion
                var u = Math.Abs(x - mean) / standardDeviation;
                var p = Normal.Standard(u);
                return sampleSize * p;
            });

            return func;
        }
    }
}
