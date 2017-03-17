// <copyright file="Client.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Graph.Types
{
    using GraphQL.Types;

    public class Client : ObjectGraphType<ClientRecord>
    {
        public Client(IRepository repository)
        {
            this.Field(x => x.Id).Description("The id of the client.");
            this.Field(x => x.Name).Description("The name of the client.");

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
