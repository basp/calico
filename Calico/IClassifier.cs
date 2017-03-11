// <copyright file="IClassifier.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System.Collections.Generic;

    public interface IClassifier<T>
    {
        IEnumerable<StyleClassRecord> Classify(IEnumerable<T> data);
    }
}
