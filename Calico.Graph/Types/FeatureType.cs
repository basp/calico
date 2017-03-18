// <copyright file="FeatureType.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Graph.Types
{
    using System.Collections.Generic;
    using GraphQL.Types;

    public class FeatureType : ObjectGraphType<FeatureTypeRecord>
    {
        public FeatureType(IRepository repository)
        {
            this.Field(x => x.Id);
            this.Field(x => x.TenantId);
            this.Field(x => x.Name);

            this.Field<ListGraphType<Attribute>>(
                name: "attributes",
                resolve: c => repository.GetAttributes(c.Source.Id));

            this.Field<ListGraphType<DataSet>>(
                name: "dataSets",
                resolve: c => this.ResolveDataSets(c, repository));
        }

        private IEnumerable<DataSetRecord> ResolveDataSets(
            ResolveFieldContext<FeatureTypeRecord> context,
            IRepository repository)
        {
            var plotId = context.GetArgument<int>("plotId");
            // var dataSets = repository.GetDataSets(plotId, )
            throw new KeyNotFoundException();
        }
    }
}
