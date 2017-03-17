// <copyright file="AttributeType.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd.Actions.GraphQL.Types
{
    using global::GraphQL.Types;

    public class AttributeType : ObjectGraphType<AttributeRecord>
    {
        public AttributeType(IRepository repository)
        {
            this.Field(x => x.Index);
            this.Field(x => x.Name);
            this.Field(x => x.DataTypeId);

            this.Field<DataTypeType>(
                name: "dataType",
                resolve: c => repository.GetDataType(c.Source.DataTypeId));
        }
    }
}
