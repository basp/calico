// <copyright file="Client.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Graph.Types
{
    using GraphQL.Types;

    public class Tenant : ObjectGraphType<TenantRecord>
    {
        public Tenant(IRepository repository)
        {
            this.Field(x => x.Id);
            this.Field(x => x.Name);

            this.Field<ListGraphType<Plot>>(
                name: "plots",
                resolve: c => repository.GetPlots(
                    c.Source.Id, 
                    first: c.GetArgument("first", defaultValue: 100)));

            this.Field<ListGraphType<FeatureType>>(
                name: "featureTypes",
                resolve: c => repository.GetFeatureTypes(
                    c.Source.Id, 
                    first: c.GetArgument("first", defaultValue: 100)));
        }
    }
}
