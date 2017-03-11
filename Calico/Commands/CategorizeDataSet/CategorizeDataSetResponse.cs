// <copyright file="CategorizeDataSetResponse.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System.Collections.Generic;

    public class CategorizeDataSetResponse
    {
        public IEnumerable<StyleClassRecord> Result { get; set; }
    }
}
