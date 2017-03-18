namespace Calico.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Repository : IRepository
    {
        private CalicoContext context;

        public Repository(CalicoContext context)
        {
            this.context = context;
        }

        public int BulkCopyAttributes(IEnumerable<AttributeRecord> recs)
        {
            throw new NotImplementedException();
        }

        public int BulkCopyAttributeValues(IEnumerable<AttributeValueRecord> recs)
        {
            throw new NotImplementedException();
        }

        public int BulkCopyFeatures(IEnumerable<FeatureRecord> recs)
        {
            throw new NotImplementedException();
        }

        public int DeleteDataSet(int id)
        {
            throw new NotImplementedException();
        }

        public int DeleteFeatureType(int id)
        {
            throw new NotImplementedException();
        }

        public int DeletePlot(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeRecord> GetAttributes(int featureTypeId)
        {
            return this.context.Attributes
                .Where(x => x.FeatureTypeId == featureTypeId)
                .OrderBy(x => x.Index)
                .ToList()
                .Select(x => new AttributeRecord
                {
                    FeatureTypeId = x.FeatureTypeId,
                    DataTypeId = x.DataTypeId,
                    Name = x.Name,
                    Index = x.Index,
                });
        }

        public IEnumerable<AttributeValueRecord> GetAttributeValues(int dataSetId)
        {
            return this.context.AttributeValues
                .Where(x => x.DataSetId == dataSetId)
                .OrderBy(x => x.FeatureIndex)
                .ToList()
                .Select(x => new AttributeValueRecord
                {
                    DataSetId = x.DataSetId,
                    FeatureIndex = x.FeatureIndex,
                    AttributeIndex = x.AttributeIndex,
                    DoubleValue = x.DoubleValue,
                    LongValue = x.LongValue,
                    StringValue = x.StringValue,
                });
        }

        public TenantRecord GetTenant(int id)
        {
            var x = this.context.Clients.Single(y => y.Id == id);
            return new TenantRecord
            {
                Id = x.Id,
                Name = x.Name,
            };
        }

        public IEnumerable<TenantRecord> GetTenants(int top, int after = 0)
        {
            return this.context.Clients
                .Take(top)
                .ToList()
                .Select(x => new TenantRecord
                {
                    Id = x.Id,
                    Name = x.Name,
                });
        }

        public DataSetRecord GetDataSet(int id)
        {
            var x = this.context.DataSets.Single(y => y.Id == id);
            return new DataSetRecord
            {
                Id = x.Id,
                PlotId = x.PlotId,
                Name = x.Name,
                FeatureTypeId = x.FeatureTypeId,
                DateCreated = x.DateCreated,
            };
        }

        public IEnumerable<DataSetRecord> GetDataSets(int plotId, int top)
        {
            return this.context.DataSets
                .Where(x => x.PlotId == plotId)
                .Take(top)
                .ToList()
                .Select(x => new DataSetRecord
                {
                    Id = x.Id,
                    PlotId = x.PlotId,
                    Name = x.Name,
                    FeatureTypeId = x.FeatureTypeId,
                    DateCreated = x.DateCreated,
                });
        }

        public IEnumerable<DataSetRecord> GetDataSets(int plotId, int featureTypeId, int top)
        {
            return this.context.DataSets
                .Where(x => x.PlotId == plotId && x.FeatureTypeId == featureTypeId)
                .Take(top)
                .ToList()
                .Select(x => new DataSetRecord
                {
                    Id = x.Id,
                    PlotId = x.PlotId,
                    Name = x.Name,
                    FeatureTypeId = x.FeatureTypeId,
                    DateCreated = x.DateCreated,
                });
        }

        public DataTypeRecord GetDataType(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DataTypeRecord> GetDataTypes()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FeatureRecord> GetFeatures(int dataSetId)
        {
            throw new NotImplementedException();
        }

        public FeatureTypeRecord GetFeatureType(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FeatureTypeRecord> GetFeatureTypes(int clientId, int top)
        {
            throw new NotImplementedException();
        }

        public PlotRecord GetPlot(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PlotRecord> GetPlots(int clientId, int top)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PlotRecord> GetPlotsContainingGeometry(int clientId, string wkt, int srid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StyleClassRecord> GetStyleClasses(int styleId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StyleRecord> GetStyles(int featureTypeId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StyleTypeRecord> GetStyleTypes()
        {
            throw new NotImplementedException();
        }

        public int InsertTenant(TenantRecord rec)
        {
            throw new NotImplementedException();
        }

        public int InsertDataSet(DataSetRecord rec)
        {
            throw new NotImplementedException();
        }

        public int InsertDataType(DataTypeRecord rec)
        {
            throw new NotImplementedException();
        }

        public int InsertFeatureType(FeatureTypeRecord rec)
        {
            throw new NotImplementedException();
        }

        public int InsertPlot(PlotRecord rec)
        {
            throw new NotImplementedException();
        }

        public int InsertStyle(StyleRecord rec)
        {
            throw new NotImplementedException();
        }

        public int InsertStyleClass(StyleClassRecord rec)
        {
            throw new NotImplementedException();
        }
    }
}
