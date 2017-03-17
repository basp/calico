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

        int DeleteDataSet(int id);

        int DeleteFeatureType(int id);

        int DeletePlot(int id);

        IEnumerable<AttributeRecord> GetAttributes(int featureTypeId);

        IEnumerable<AttributeValueRecord> GetAttributeValues(int dataSetId);

        ClientRecord GetClient(int id);

        IEnumerable<ClientRecord> GetClients(int top);

        DataSetRecord GetDataSet(int id);

        IEnumerable<DataSetRecord> GetDataSets(int plotId, int top);

        DataTypeRecord GetDataType(int id);

        IEnumerable<DataTypeRecord> GetDataTypes();

        IEnumerable<FeatureRecord> GetFeatures(int dataSetId);

        FeatureTypeRecord GetFeatureType(int id);

        IEnumerable<FeatureTypeRecord> GetFeatureTypes(int clientId, int top);

        PlotRecord GetPlot(int id);

        IEnumerable<PlotRecord> GetPlots(int clientId, int top);

        IEnumerable<PlotRecord> GetPlotsContainingGeometry(int clientId, string wkt, int srid);

        IEnumerable<StyleClassRecord> GetStyleClasses(int styleId);

        IEnumerable<StyleRecord> GetStyles(int featureTypeId);

        IEnumerable<StyleTypeRecord> GetStyleTypes();

        int InsertClient(ClientRecord rec);

        int InsertDataSet(DataSetRecord rec);

        int InsertDataType(DataTypeRecord rec);

        int InsertFeatureType(FeatureTypeRecord rec);

        int InsertPlot(PlotRecord rec);

        int InsertStyle(StyleRecord rec);

        int InsertStyleClass(StyleClassRecord rec);
    }
}
