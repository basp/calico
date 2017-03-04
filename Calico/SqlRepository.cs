// <copyright file="SqlRepository.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Data.SqlTypes;
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

        public int BulkCopyAttributes(IEnumerable<AttributeRecord> recs)
        {
            var table = CreateAttributeTable();
            foreach (var a in recs)
            {
                var row = table.NewRow();
                row["FeatureTypeId"] = a.FeatureTypeId;
                row["Index"] = a.Index;
                row["DataTypeId"] = a.DataTypeId;
                row["Name"] = a.Name;
                table.Rows.Add(row);
            }

            var opts = SqlBulkCopyOptions.Default;
            using (var copy = new SqlBulkCopy(this.connection, opts, this.transaction))
            {
                copy.DestinationTableName = "Attributes";
                copy.WriteToServer(table);
            }

            return table.Rows.Count;
        }

        public int BulkCopyAttributeValues(IEnumerable<AttributeValueRecord> recs)
        {
            var table = CreateAttributeValueTable();
            foreach (var v in recs)
            {
                var row = table.NewRow();
                row["DataSetId"] = v.DataSetId;
                row["FeatureIndex"] = v.FeatureIndex;
                row["AttributeIndex"] = v.AttributeIndex;

                if (v.DoubleValue.HasValue)
                {
                    row["DoubleValue"] = v.DoubleValue;
                }

                if (v.LongValue.HasValue)
                {
                    row["LongValue"] = v.LongValue;
                }

                if (!string.IsNullOrEmpty(v.StringValue))
                {
                    row["StringValue"] = v.StringValue;
                }

                table.Rows.Add(row);
            }

            var opts = SqlBulkCopyOptions.Default;
            using (var copy = new SqlBulkCopy(this.connection, opts, this.transaction))
            {
                copy.DestinationTableName = "AttributeValues";
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
                row["Geometry"] = ValidSqlGeometryFromWkt(f.Wkt, f.SRID);
                row["SRID"] = f.SRID;
                table.Rows.Add(row);
            }

            var opts = SqlBulkCopyOptions.Default;
            using (var copy = new SqlBulkCopy(this.connection, opts, this.transaction))
            {
                copy.DestinationTableName = "Features";
                copy.WriteToServer(table);
            }

            return table.Rows.Count;
        }

        public int DeleteDataSet(int id)
        {
            var @param = new { Id = id };
            return this.connection.Execute(
                nameof(this.DeleteDataSet),
                @param,
                commandType: CommandType.StoredProcedure,
                transaction: this.transaction);
        }

        public int DeletePlot(int id)
        {
            var @param = new { Id = id };
            return this.connection.Execute(
                nameof(this.DeletePlot),
                @param,
                commandType: CommandType.StoredProcedure,
                transaction: this.transaction);
        }

        public IEnumerable<AttributeRecord> GetAttributes(int featureTypeId)
        {
            var @param = new { FeatureTypeId = featureTypeId };
            return this.connection.Query<AttributeRecord>(
                nameof(this.GetAttributes),
                @param,
                commandType: CommandType.StoredProcedure,
                transaction: this.transaction);
        }

        public ClientRecord GetClient(int id)
        {
            var @param = new { Id = id };
            return this.connection.QuerySingle<ClientRecord>(
                nameof(this.GetClient),
                @param,
                commandType: CommandType.StoredProcedure,
                transaction: this.transaction);
        }

        public IEnumerable<ClientRecord> GetClients(int top)
        {
            var @param = new { Top = top };
            return this.connection.Query<ClientRecord>(
                nameof(this.GetClients),
                @param,
                commandType: CommandType.StoredProcedure,
                transaction: this.transaction);
        }

        public DataSetRecord GetDataSet(int id)
        {
            var @param = new { Id = id };
            return this.connection.QuerySingle<DataSetRecord>(
                nameof(this.GetDataSet),
                @param,
                commandType: CommandType.StoredProcedure,
                transaction: this.transaction);
        }

        public IEnumerable<DataSetRecord> GetDataSets(int plotId, int top)
        {
            var @param = new { PlotId = plotId, Top = top };
            return this.connection.Query<DataSetRecord>(
                nameof(this.GetDataSets),
                @param,
                commandType: CommandType.StoredProcedure,
                transaction: this.transaction);
        }

        public IEnumerable<DataTypeRecord> GetDataTypes()
        {
            return this.connection.Query<DataTypeRecord>(
                nameof(this.GetDataTypes),
                commandType: CommandType.StoredProcedure,
                transaction: this.transaction);
        }

        public FeatureTypeRecord GetFeatureType(int id)
        {
            var @param = new { Id = id };
            return this.connection.QuerySingle<FeatureTypeRecord>(
                nameof(this.GetFeatureType),
                @param,
                commandType: CommandType.StoredProcedure,
                transaction: this.transaction);
        }

        public IEnumerable<FeatureTypeRecord> GetFeatureTypes(int clientId, int top)
        {
            var @param = new { ClientId = clientId, Top = top };
            return this.connection.Query<FeatureTypeRecord>(
                nameof(this.GetFeatureTypes),
                @param,
                commandType: CommandType.StoredProcedure,
                transaction: this.transaction);
        }

        public PlotRecord GetPlot(int plotId)
        {
            var @param = new { PlotId = plotId };
            return this.connection.QuerySingle<PlotRecord>(
                nameof(this.GetPlot),
                @param,
                commandType: CommandType.StoredProcedure,
                transaction: this.transaction);
        }

        public IEnumerable<PlotRecord> GetPlots(int clientId, int top)
        {
            var @param = new { ClientId = clientId, Top = top };
            return this.connection.Query<PlotRecord>(
                nameof(this.GetPlots),
                @param,
                commandType: CommandType.StoredProcedure,
                transaction: this.transaction);
        }

        public IEnumerable<PlotRecord> GetPlotsContainingGeometry(
            int clientId,
            string wkt)
        {
            var geometry = SqlGeometry.Parse(wkt).MakeValid();
            var @param = new { ClientId = clientId, Geometry = geometry };
            return this.connection.Query<PlotRecord>(
                nameof(this.GetPlotsContainingGeometry),
                @param,
                commandType: CommandType.StoredProcedure,
                transaction: this.transaction);
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
            var geo = ValidSqlGeometryFromWkt(rec.Wkt, rec.SRID);
            var @param = new { rec.ClientId, rec.Name, Geometry = geo, rec.SRID };
            return this.Insert(nameof(this.InsertPlot), @param);
        }

        private static DataTable CreateAttributeTable()
        {
            var cols = new Dictionary<string, Type>
            {
                ["FeatureTypeId"] = typeof(int),
                ["Index"] = typeof(int),
                ["DataTypeId"] = typeof(int),
                ["Name"] = typeof(string),
            }
            .Select(x => new DataColumn(x.Key, x.Value))
            .ToArray();

            var table = new DataTable();
            table.Columns.AddRange(cols);
            return table;
        }

        private static DataTable CreateAttributeValueTable()
        {
            var cols = new Dictionary<string, Type>
            {
                ["DataSetId"] = typeof(int),
                ["FeatureIndex"] = typeof(int),
                ["AttributeIndex"] = typeof(int),
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
                ["SRID"] = typeof(int),
            }
            .Select(x => new DataColumn(x.Key, x.Value))
            .ToArray();

            var table = new DataTable();
            table.Columns.AddRange(cols);
            return table;
        }

        private static SqlGeometry ValidSqlGeometryFromWkt(string wkt, int srid)
        {
            var text = new SqlChars(new SqlString(wkt));
            return SqlGeometry.STGeomFromText(text, srid).MakeValid();
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
