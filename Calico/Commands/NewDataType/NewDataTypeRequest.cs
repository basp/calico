// <copyright file="NewDataTypeRequest.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    public class NewDataTypeRequest
    {
        public string Name { get; set; }

        public string SqlType { get; set; }

        public string BclType { get; set; }
    }
}
