// <copyright file="FeatureType.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Graph.Types
{
    using GraphQL.Types;

    public class FeatureType : ObjectGraphType<FeatureTypeRecord>
    {
        public FeatureType(IRepository repository)
        {
            this.Field(x => x.Id);
            this.Field(x => x.ClientId);
            this.Field(x => x.Name);

            this.Field<ListGraphType<Attribute>>(
                name: "attributes",
                resolve: c => repository.GetAttributes(c.Source.Id));
        }
    }
}
