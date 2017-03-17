// <copyright file="Plot.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Graph.Types
{
    using GraphQL.Types;

    public class Plot : ObjectGraphType<PlotRecord>
    {
        public Plot(IRepository repository)
        {
            this.Field(x => x.Id);
            this.Field(x => x.Name);
            this.Field(x => x.ClientId);
            this.Field(x => x.Wkt);

            this.Field<ListGraphType<DataSet>>(
                name: "dataSets",
                resolve: c => repository.GetDataSets(
                    c.Source.Id,
                    c.GetArgument<int>("first", defaultValue: 100)));
        }
    }
}
