// <copyright file="QuantileClassifier.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Classification
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class QuantileClassifier : IQuantifyingClassifier
    {
        private readonly int numberOfClasses;

        public QuantileClassifier(int numberOfClasses = 5)
        {
            this.numberOfClasses = numberOfClasses;
        }

        public IDictionary<double, int> Classify(IEnumerable<double> data)
        {
            throw new NotImplementedException();
        }
    }
}
