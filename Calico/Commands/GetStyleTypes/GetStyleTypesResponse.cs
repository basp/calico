// <copyright file="GetStyleTypesResponse.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System.Collections.Generic;

    public class GetStyleTypesResponse
    {
        public IEnumerable<StyleTypeRecord> StyleTypes { get; set; }
    }
}
