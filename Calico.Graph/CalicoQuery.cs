namespace Calico.Graph
{
    using GraphQL.Types;
    using Types;

    public class CalicoQuery : ObjectGraphType
    {
        public CalicoQuery(IRepository repository)
        {
            this.Name = "Query";

            this.Field<Tenant>(
                name: "tenant",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>>()
                    {
                        Name = "id",
                    }),
                resolve: c => repository.GetTenant(
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

            this.Field<ListGraphType<Tenant>>(
                name: "tenants",
                resolve: c => repository.GetTenants(
                    first: c.GetArgument("first", defaultValue: 100), after: 0));
        }
    }
}