// <copyright file="GetAttributesResponse.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System.Collections.Generic;

    public class GetAttributesResponse
    {
        public IEnumerable<AttributeRecord> Attributes { get; set; }
    }
}
