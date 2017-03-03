// <copyright file="ImportAttributesResponse.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System.Collections.Generic;

    public class ImportAttributesResponse
    {
        public IEnumerable<AttributeRecord> Attributes { get; set; }
    }
}
