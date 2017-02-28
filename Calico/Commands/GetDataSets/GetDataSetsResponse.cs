// <copyright file="GetDataSetsResponse.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System.Collections.Generic;

    public class GetDataSetsResponse
    {
        public IEnumerable<DataSetRecord> DataSets { get; set; }
    }
}
