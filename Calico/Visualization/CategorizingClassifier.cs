// <copyright file="CategorizingClassifier.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Visualization
{
    using System.Collections.Generic;
    using System.Linq;

    public class CategorizingClassifier : IClassifier<string>
    {
        public IEnumerable<StyleClassRecord> Classify(IEnumerable<string> data)
        {
            return data.Distinct()
                .Select(x => new StyleClassRecord
                {
                    Category = x,
                    Legend = x,
                });
        }
    }
}
