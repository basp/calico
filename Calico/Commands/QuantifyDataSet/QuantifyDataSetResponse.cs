// <copyright file="QuantifyDataSetResponse.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System.Collections.Generic;

    public class QuantifyDataSetResponse<T>
    {
        public IDictionary<T, int> Result { get; set; }
    }
}
