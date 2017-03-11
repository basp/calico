// <copyright file="GetClassesResponse.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System.Collections.Generic;

    public class GetClassesResponse
    {
        public IEnumerable<StyleClassRecord> Classes { get; set; }

        public IEnumerable<double> Outliers { get; set; }
    }
}
