namespace Calico
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using Dapper;
    using Microsoft.SqlServer.Types;

    public class SqlRepository : IRepository
    {
        private readonly SqlConnection connection;
        private readonly SqlTransaction transaction;

        public SqlRepository(
            SqlConnection connection,
            SqlTransaction transaction = null)
        {
            this.connection = connection;
            this.transaction = transaction;
        }

        private static DataTable CreateAttributeValueTable()
        {
            var cols = new Dictionary<string, Type>
            {
                ["DataSetId"] = typeof(int),
                ["AttributeId"] = typeof(int),
                ["FeatureIndex"] = typeof(int),
                ["DoubleValue"] = typeof(double),
                ["LongValue"] = typeof(long),
                ["StringValue"] = typeof(string),
            }
            .Select(x => new DataColumn(x.Key, x.Value))
            .ToArray();

            var table = new DataTable();
            table.Columns.AddRange(cols);
            return table;
        }

        private static DataTable CreateFeatureTable()
        {
            var cols = new Dictionary<string, Type>
            {
                ["DataSetId"] = typeof(int),
                ["Index"] = typeof(int),
                ["Geometry"] = typeof(SqlGeometry),
            }
            .Select(x => new DataColumn(x.Key, x.Value))
            .ToArray();

            var table = new DataTable();
            table.Columns.AddRange(cols);
            return table;
        }

        public int BulkCopyAttributeValues(IEnumerable<AttributeValueRecord> recs)
        {
            var table = CreateAttributeValueTable();
            foreach (var v in recs)
            {
                var row = table.NewRow();
                row["DataSetId"] = v.DataSetId;
                row["AttributeId"] = v.AttributeId;
                row["FeatureIndex"] = v.FeatureIndex;
                row["DoubleValue"] = v.DoubleValue;
                row["LongValue"] = v.LongValue;
                row["StringValue"] = v.StringValue;
                table.Rows.Add(row);
            }

            var opts = SqlBulkCopyOptions.Default;
            using (var copy = new SqlBulkCopy(this.connection, opts, this.transaction))
            {
                copy.WriteToServer(table);
            }

            return table.Rows.Count;
        }

        public int BulkCopyFeatures(IEnumerable<FeatureRecord> recs)
        {
            var table = CreateFeatureTable();
            foreach (var f in recs)
            {
                var row = table.NewRow();
                row["DataSetId"] = f.DataSetId;
                row["Index"] = f.Index;
                row["Geometry"] = f.Geometry;
                table.Rows.Add(row);
            }

            var opts = SqlBulkCopyOptions.Default;
            using (var copy = new SqlBulkCopy(this.connection, opts, this.transaction))
            {
                copy.WriteToServer(table);
            }

            return table.Rows.Count;
        }

        public IEnumerable<ClientRecord> GetClients()
        {
            return this.connection.Query<ClientRecord>(
                nameof(this.GetClients),
                commandType: CommandType.StoredProcedure,
                transaction: this.transaction);
        }

        public IEnumerable<PlotRecord> GetPlots(int clientId)
        {
            var @param = new { ClientId = clientId };
            return this.connection.Query<PlotRecord>(
                nameof(this.GetPlots),
                @param,
                commandType: CommandType.StoredProcedure,
                transaction: this.transaction);
        }

        public int InsertAttribute(AttributeRecord rec)
        {
            throw new NotImplementedException();
        }

        public int InsertClient(ClientRecord rec)
        {
            var @param = new { rec.Name };
            return this.Insert(nameof(this.InsertClient), @param);
        }

        public int InsertDataSet(DataSetRecord rec)
        {
            var @param = new { rec.PlotId, rec.FeatureTypeId, rec.Name, rec.DateCreated };
            return this.Insert(nameof(this.InsertDataSet), @param);
        }

        public int InsertDataType(DataTypeRecord rec)
        {
            var @param = new { rec.Name, rec.SqlType, rec.BclType };
            return this.Insert(nameof(this.InsertDataType), @param);
        }

        public int InsertFeatureType(FeatureTypeRecord rec)
        {
            var @param = new { rec.ClientId, rec.Name };
            return this.Insert(nameof(this.InsertFeatureType), @param);
        }

        public int InsertPlot(PlotRecord rec)
        {
            var @param = new { rec.ClientId, rec.Name, rec.Geometry };
            return this.Insert(nameof(this.InsertPlot), @param);
        }

        public int UpsertAttributeValue(AttributeValueRecord rec)
        {
            throw new NotImplementedException();
        }

        public int UpsertFeature(FeatureRecord rec)
        {
            throw new NotImplementedException();
        }

        private int Insert(string sproc, object @param)
        {
            return this.connection.QueryFirst<int>(
                sproc,
                @param,
                commandType: CommandType.StoredProcedure,
                transaction: this.transaction);
        }
    }
}
