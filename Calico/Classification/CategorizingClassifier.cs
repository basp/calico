// <copyright file="CategorizingClassifier.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Classification
{
    using System.Collections.Generic;
    using System.Linq;

    public class CategorizingClassifier : ICategorizingClassifier
    {
        public IDictionary<string, int> Classify(IEnumerable<string> data)
        {
            return data.Distinct()
                .Select((x, i) => new { Key = x, Index = i })
                .ToDictionary(x => x.Key, x => x.Index);
        }
    }
}
