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
            this.Name = "Query";

            this.Field<Client>(
                name: "client",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>>()
                    {
                        Name = "id",
                    }),
                resolve: c => repository.GetClient(
                    c.GetArgument<int>("id")));

            this.Field<DataSet>(
                name: "dataSet",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>>()
                    {
                        Name = "id",
                    }),
                resolve: c => repository.GetDataSet(
                    c.GetArgument<int>("id")));

            this.Field<FeatureType>(
                name: "featureType",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>>()
                    {
                        Name = "id",
                    }),
                resolve: c => repository.GetFeatureType(
                    c.GetArgument<int>("id")));

            this.Field<Plot>(
                name: "plot",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>>()
                    {
                        Name = "id",
                    }),
                resolve: c => repository.GetPlot(
                    c.GetArgument<int>("id")));

            this.Field<ListGraphType<DataType>>(
                name: "dataTypes",
                resolve: c => repository.GetDataTypes());

            this.Field<ListGraphType<Client>>(
                name: "clients",
                resolve: c => repository.GetClients(top: 100));
        }
    }
}
