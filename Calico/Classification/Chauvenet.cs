// <copyright file="Chauvenet.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Classification
{
    using System;

    /// <summary>
    /// Implements Chauvenet's criterion for identifying outliers in a sample.
    /// </summary>
    public class Chauvenet
    {
        private readonly Func<double, double> normal;

        public Chauvenet(Func<double, double> normal)
        {
            this.normal = normal;
        }

        public Func<double, double> Create(int sampleSize, double mean, double standardDeviation)
        {
            var func = new Func<double, double>(x =>
            {
                // https://en.wikipedia.org/wiki/Chauvenet%27s_criterion
                //
                // How many times fits the standard deviation into the
                // difference between `x` and the mean of the sample (set)?
                var u = Math.Abs(x - mean) / standardDeviation;

                // What is the probability score of this occuring in a
                // standard distribution?
                var p = this.normal(u);

                // Return a score that involves the size of the sample
                // and the probability of the value `x` appearing in such
                // a distrubution.
                return sampleSize * p;
            });

            return func;
        }
    }
}
