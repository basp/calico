// <copyright file="QuantifyDataSetResponse.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System.Collections.Generic;

    public class QuantifyDataSetResponse
    {
        public IEnumerable<StyleClassRecord> Classes { get; set; }

        public IEnumerable<double> Outliers { get; set; }
    }
}
