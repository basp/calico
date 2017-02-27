﻿namespace Calico
{
    using System.Collections.Generic;

    public interface IRepository
    {
        int BulkCopyAttributeValues(IEnumerable<AttributeValueRecord> recs);

        int BulkCopyFeatures(IEnumerable<FeatureRecord> recs);

        int InsertAttribute(AttributeRecord rec);

        int InsertClient(ClientRecord rec);

        int InsertDataSet(DataSetRecord rec);

        int InsertDataType(DataTypeRecord rec);

        int InsertFeatureType(FeatureTypeRecord rec);

        int InsertPlot(PlotRecord rec);

        int UpsertAttributeValue(AttributeValueRecord rec);

        int UpsertFeature(FeatureRecord rec);
    }
}
