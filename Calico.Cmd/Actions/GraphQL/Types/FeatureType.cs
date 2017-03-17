// <copyright file="FeatureType.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd.Actions.GraphQL.Types
{
    using global::GraphQL.Types;

    public class FeatureType : ObjectGraphType<FeatureRecord>
    {
        public FeatureType(IRepository repository)
        {
            this.Field(x => x.DataSetId);
            this.Field(x => x.Index);
            this.Field(x => x.Wkt);
        }
    }
}
