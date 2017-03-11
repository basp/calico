// <copyright file="AttributeStatistics.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    public class AttributeStatistics
    {
        public AttributeProxy Attribute { get; set; }

        public double Mean { get; set; }

        public double Variance { get; set; }

        public double StandardDeviation { get; set; }

        public double Skewness { get; set; }

        public double Kurtosis { get; set; }

        public double Maximum { get; set; }

        public double Minimum { get; set; }
    }
}
