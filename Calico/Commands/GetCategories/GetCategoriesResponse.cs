// <copyright file="GetCategoriesResponse.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System.Collections.Generic;

    public class GetCategoriesResponse
    {
        public IEnumerable<StyleClassRecord> Result { get; set; }
    }
}
