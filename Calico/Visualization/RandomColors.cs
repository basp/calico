// <copyright file="RandomColors.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Visualization
{
    using System;

    public class RandomColors : IColorRamp
    {
        private static readonly Random Rng = new Random();

        public Tuple<int, int, int> GetColor(double n)
        {
            var r = Rng.Next(256);
            var g = Rng.Next(256);
            var b = Rng.Next(256);
            return Tuple.Create(r, g, b);
        }
    }
}
