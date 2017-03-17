// <copyright file="DataSet.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Graph.Types
{
    using GraphQL.Types;

    public class DataSet : ObjectGraphType<DataSetRecord>
    {
        public DataSet(IRepository repository)
        {
            this.Field(x => x.Id);
            this.Field(x => x.PlotId);
            this.Field(x => x.FeatureTypeId);
            this.Field(x => x.Name);
            this.Field(x => x.DateCreated);

            this.Field<Plot>(
                name: "plot",
                resolve: c => repository.GetPlot(c.Source.PlotId));

            this.Field<FeatureType>(
                name: "featureType",
                resolve: c => repository.GetFeatureType(c.Source.FeatureTypeId));

            this.Field<ListGraphType<Feature>>(
                name: "features",
                resolve: c => repository.GetFeatures(c.Source.Id));
        }
    }
}
