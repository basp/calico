// <copyright file="CategorizeDataSetResponse.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using System.Collections.Generic;

    public class CategorizeDataSetResponse<T>
    {
        public IDictionary<T, int> Result { get; set; }
    }
}
