namespace Calico
{
    using System.Collections.Generic;

    public interface IRepository
    {
        int BulkCopyAttributeValues(IEnumerable<AttributeValueRecord> recs);

        int BulkCopyFeatures(IEnumerable<FeatureRecord> recs);

        IEnumerable<ClientRecord> GetClients(int top);

        IEnumerable<DataSetRecord> GetDataSets(int plotId, int top);

        IEnumerable<PlotRecord> GetPlots(int clientId, int top);

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
