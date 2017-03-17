// <copyright file="PlotType.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd.Actions.GraphQL.Types
{
    using global::GraphQL.Types;

    internal class PlotType : ObjectGraphType<PlotRecord>
    {
        public PlotType(IRepository repository)
        {
            this.Field(x => x.Id);
            this.Field(x => x.Name);
            this.Field(x => x.Wkt);

            this.Field<ListGraphType<DataSetType>>(
                name: "dataSets",
                resolve: c => repository.GetDataSets(c.Source.Id, top: 100));
        }
    }
}
