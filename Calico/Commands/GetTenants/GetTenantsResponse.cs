// <copyright file="GetTenantsResponse.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System.Collections.Generic;

    public class GetTenantsResponse
    {
        public IEnumerable<TenantRecord> Tenants { get; set; }
    }
}
