// <copyright file="IRepository.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System.Collections.Generic;
    using Optional;

    public interface IRepository
    {
        int BulkCopyAttributes(IEnumerable<AttributeRecord> recs);

        int BulkCopyAttributeValues(IEnumerable<AttributeValueRecord> recs);

        int BulkCopyFeatures(IEnumerable<FeatureRecord> recs);

        IEnumerable<AttributeRecord> GetAttributes(int featureTypeId);

        IEnumerable<ClientRecord> GetClients(int top);

        DataSetRecord GetDataSet(int id);

        IEnumerable<DataSetRecord> GetDataSets(int plotId, int top);

        IEnumerable<DataTypeRecord> GetDataTypes();

        FeatureTypeRecord GetFeatureType(int id);

        IEnumerable<FeatureTypeRecord> GetFeatureTypes(int clientId, int top);

        IEnumerable<PlotRecord> GetPlots(int clientId, int top);

        int InsertClient(ClientRecord rec);

        int InsertDataSet(DataSetRecord rec);

        int InsertDataType(DataTypeRecord rec);

        int InsertFeatureType(FeatureTypeRecord rec);

        int InsertPlot(PlotRecord rec);
    }
}
