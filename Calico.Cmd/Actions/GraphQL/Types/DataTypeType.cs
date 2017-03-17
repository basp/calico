// <copyright file="DataTypeType.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd.Actions.GraphQL.Types
{
    using global::GraphQL.Types;

    public class DataTypeType : ObjectGraphType<DataTypeRecord>
    {
        public DataTypeType()
        {
            this.Field(x => x.Id);
            this.Field(x => x.Name);
            this.Field(x => x.SqlType);
            this.Field(x => x.BclType);
        }
    }
}
