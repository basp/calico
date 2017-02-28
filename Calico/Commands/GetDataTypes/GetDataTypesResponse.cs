// <copyright file="GetDataTypesResponse.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System.Collections.Generic;

    public class GetDataTypesResponse
    {
        public IEnumerable<DataTypeRecord> DataTypes { get; set; }
    }
}
