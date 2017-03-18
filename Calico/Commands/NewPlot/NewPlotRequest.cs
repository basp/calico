// <copyright file="NewPlotRequest.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    public class NewPlotRequest
    {
        public int TenantId { get; set; }

        public string Name { get; set; }

        public int SRID { get; set; }
    }
}
