// <copyright file="AttributeProxy.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using System.Data;

    public class AttributeProxy
    {
        public AttributeProxy(string name, Type type)
        {
            this.Name = name;
            this.Type = type.FullName;
        }

        public string Name { get; set; }

        public string Type { get; set; }

        public static AttributeProxy Create(DataColumn column)
        {
            return new AttributeProxy(column.ColumnName, column.DataType);
        }
    }
}
