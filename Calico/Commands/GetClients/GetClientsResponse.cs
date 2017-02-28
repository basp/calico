// <copyright file="GetClientsResponse.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System.Collections.Generic;

    public class GetClientsResponse
    {
        public IEnumerable<ClientRecord> Clients { get; set; }
    }
}
