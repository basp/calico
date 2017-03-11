// <copyright file="SingleSymbolClassifier.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Visualization
{
    using System.Collections.Generic;

    public class SingleSymbolClassifier<T> : IClassifier<T>
    {
        private readonly StyleClassRecord symbol;

        public SingleSymbolClassifier(StyleClassRecord symbol)
        {
            this.symbol = symbol;
        }

        public static SingleSymbolClassifier<T> Create(StyleClassRecord symbol) =>
            new SingleSymbolClassifier<T>(symbol);

        public IEnumerable<StyleClassRecord> Classify(IEnumerable<T> data)
        {
            return new[] { this.symbol };
        }
    }
}
