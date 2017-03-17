// <copyright file="Feature.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Graph.Types
{
    using GraphQL.Types;

    public class Feature : ObjectGraphType<FeatureRecord>
    {
        public Feature(IRepository repository)
        {
            this.Field(x => x.DataSetId);
            this.Field(x => x.Index);
            this.Field(x => x.Wkt);
        }
    }
}
