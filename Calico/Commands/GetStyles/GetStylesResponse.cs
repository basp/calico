// <copyright file="GetStylesResponse.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System.Collections.Generic;

    public class GetStylesResponse
    {
        public IEnumerable<StyleRecord> Styles { get; set; }
    }
}
