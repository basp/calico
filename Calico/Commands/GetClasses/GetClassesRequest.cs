// <copyright file="GetClassesRequest.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    public class GetClassesRequest
    {
        public string PathToShapefile { get; set; }

        public string ColumnName { get; set; }

        public bool Normalize { get; set; }
    }
}
