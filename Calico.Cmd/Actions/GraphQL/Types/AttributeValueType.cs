// <copyright file="AttributeValueType.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd.Actions.GraphQL.Types
{
    using global::GraphQL.Types;

    public class AttributeValueType : ObjectGraphType<AttributeValueRecord>
    {
        public AttributeValueType(IRepository repository)
        {
            this.Field(x => x.DataSetId);
            this.Field(x => x.FeatureIndex);
            this.Field(x => x.AttributeIndex);
            this.Field(x => x.DoubleValue);
            this.Field(x => x.LongValue);
            this.Field(x => x.StringValue);

            this.Field<DataSetType>(
                name: "dataSet",
                resolve: c => repository.GetDataSet(c.Source.DataSetId));
        }
    }
}
