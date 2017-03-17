// <copyright file="DataSetType.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd.Actions.GraphQL.Types
{
    using global::GraphQL.Types;

    internal class DataSetType : ObjectGraphType<DataSetRecord>
    {
        public DataSetType(IRepository repository)
        {
            this.Field(x => x.Id);
            this.Field(x => x.PlotId);
            this.Field(x => x.FeatureTypeId);
            this.Field(x => x.Name);
            this.Field(x => x.DateCreated);

            this.Field<FeatureTypeType>(
                name: "featureType",
                resolve: c => repository.GetFeatureType(c.Source.Id));
        }
    }
}
