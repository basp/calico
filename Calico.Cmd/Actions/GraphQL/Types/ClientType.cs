// <copyright file="ClientType.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd.Actions.GraphQL.Types
{
    using global::GraphQL.Types;

    internal class ClientType : ObjectGraphType<ClientRecord>
    {
        private const int Top = 100;

        public ClientType(IRepository repository)
        {
            this.Field(x => x.Id).Description("The id of the client.");
            this.Field(x => x.Name).Description("The name of the client.");

            this.Field<ListGraphType<PlotType>>(
                name: "plots",
                resolve: c => repository.GetPlots(c.Source.Id, top: Top));

            this.Field<ListGraphType<FeatureTypeType>>(
                name: "featureTypes",
                resolve: c => repository.GetFeatureTypes(c.Source.Id, top: Top));
        }
    }
}
