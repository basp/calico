// <copyright file="FeatureTypeType.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd.Actions.GraphQL.Types
{
    using global::GraphQL.Types;

    internal class FeatureTypeType : ObjectGraphType<FeatureTypeRecord>
    {
        public FeatureTypeType(IRepository repository)
        {
            this.Field(x => x.Id);
            this.Field(x => x.ClientId);
            this.Field(x => x.Name);

            this.Field<ListGraphType<AttributeType>>(
                name: "attributes",
                resolve: c => repository.GetAttributes(c.Source.Id));
        }
    }
}
