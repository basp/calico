// <copyright file="IColorRamp.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Visualization
{
    using System;

    public interface IColorRamp
    {
        Tuple<int, int, int> GetColor(double n);
    }
}
