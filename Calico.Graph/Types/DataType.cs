﻿// <copyright file="DataType.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Graph.Types
{
    using GraphQL.Types;

    public class DataType : ObjectGraphType<DataTypeRecord>
    {
        public DataType()
        {
            this.Field(x => x.Id).Description("foo");
            this.Field(x => x.Name);
            this.Field(x => x.SqlType);
            this.Field(x => x.BclType);
        }
    }
}
