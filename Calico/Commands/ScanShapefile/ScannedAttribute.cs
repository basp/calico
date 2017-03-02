// <copyright file="ScannedAttribute.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;

    public class ScannedAttribute
    {
        public ScannedAttribute(string name, Type type)
        {
            this.Name = name;
            this.Type = type.FullName;
        }

        public string Name { get; set; }

        public string Type { get; set; }
    }
}
