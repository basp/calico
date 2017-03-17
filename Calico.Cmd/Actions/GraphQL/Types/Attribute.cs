// <copyright file="Attribute.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd.Actions.GraphQL.Types
{
    using global::GraphQL.Types;

    public class Attribute : ObjectGraphType<AttributeRecord>
    {
        public Attribute(IRepository repository)
        {
            this.Field(x => x.Index);
            this.Field(x => x.Name);
            this.Field(x => x.DataTypeId);

            this.Field<DataType>(
                name: "dataType",
                resolve: c => repository.GetDataType(c.Source.DataTypeId));
        }
    }
}
