// <copyright file="GetStatisticsResponse.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System.Collections.Generic;

    public class GetStatisticsResponse
    {
        public IEnumerable<AttributeStatistics> Result { get; set; }
    }
}
