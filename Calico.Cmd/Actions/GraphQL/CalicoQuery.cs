// <copyright file="CalicoQuery.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd.Actions.GraphQL
{
    using global::GraphQL.Types;
    using Types;

    public class CalicoQuery : ObjectGraphType
    {
        public CalicoQuery(IRepository repository)
        {
            this.Field<ClientType>(
                name: "client",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>>()
                    {
                        Name = "id",
                    }),
                resolve: c => repository.GetClient(
                    c.GetArgument<int>("id")));

            this.Field<DataSetType>(
                name: "dataSet",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>>()
                    {
                        Name = "id",
                    }),
                resolve: c => repository.GetDataSet(
                    c.GetArgument<int>("id")));

            this.Field<FeatureTypeType>(
                name: "featureType",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>>()
                    {
                        Name = "id",
                    }),
                resolve: c => repository.GetFeatureType(
                    c.GetArgument<int>("id")));

            this.Field<PlotType>(
                name: "plot",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>>()
                    {
                        Name = "id",
                    }),
                resolve: c => repository.GetPlot(
                    c.GetArgument<int>("id")));

            this.Field<ListGraphType<DataTypeType>>(
                name: "dataTypes",
                resolve: c => repository.GetDataTypes());

            this.Field<ListGraphType<ClientType>>(
                name: "clients",
                resolve: c => repository.GetClients(top: 100));
        }
    }
}
