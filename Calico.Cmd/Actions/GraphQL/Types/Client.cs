// <copyright file="Client.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd.Actions.GraphQL.Types
{
    using global::GraphQL.Types;

    internal class Client : ObjectGraphType<ClientRecord>
    {
        private const int Top = 100;

        public Client(IRepository repository)
        {
            this.Field(x => x.Id).Description("The id of the client.");
            this.Field(x => x.Name).Description("The name of the client.");

            this.Field<ListGraphType<Plot>>(
                name: "plots",
                resolve: c => repository.GetPlots(c.Source.Id, top: Top));

            this.Field<ListGraphType<FeatureType>>(
                name: "featureTypes",
                resolve: c => repository.GetFeatureTypes(c.Source.Id, top: Top));
        }
    }
}
