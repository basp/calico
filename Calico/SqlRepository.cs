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
        private readonly SqlSession session;

        public SqlRepository(SqlSession session)
        {
            this.session = session;
        }

        public static SqlRepository Create(SqlConnection conn)
        {
            return new SqlRepository(SqlSession.Open(conn));
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

            return this.session.BulkCopy("Attributes", table);
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

            return this.session.BulkCopy("AttributeValues", table);
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
                row["Geography"] = ValidSqlGeographyFromWkt(f.Wkt, f.SRID);
                row["SRID"] = f.SRID;
                table.Rows.Add(row);
            }

            return this.session.BulkCopy("Features", table);
        }

        public int DeleteDataSet(int id)
        {
            var @param = new { Id = id };
            return this.session.Execute(
                nameof(this.DeleteDataSet),
                @param);
        }

        public int DeleteFeatureType(int id)
        {
            var @param = new { Id = id };
            return this.session.Execute(
                nameof(this.DeleteFeatureType),
                @param);
        }

        public int DeletePlot(int id)
        {
            var @param = new { Id = id };
            return this.session.Execute(
                nameof(this.DeletePlot),
                @param);
        }

        public IEnumerable<AttributeRecord> GetAttributes(int featureTypeId)
        {
            var @param = new { FeatureTypeId = featureTypeId };
            return this.session.Query<AttributeRecord>(
                nameof(this.GetAttributes),
                @param);
        }

        public ClientRecord GetClient(int id)
        {
            var @param = new { Id = id };
            return this.session.QuerySingle<ClientRecord>(
                nameof(this.GetClient),
                @param);
        }

        public IEnumerable<ClientRecord> GetClients(int top)
        {
            var @param = new { Top = top };
            return this.session.Query<ClientRecord>(
                nameof(this.GetClients),
                @param);
        }

        public DataSetRecord GetDataSet(int id)
        {
            var @param = new { Id = id };
            return this.session.QuerySingle<DataSetRecord>(
                nameof(this.GetDataSet),
                @param);
        }

        public IEnumerable<DataSetRecord> GetDataSets(int plotId, int top)
        {
            var @param = new { PlotId = plotId, Top = top };
            return this.session.Query<DataSetRecord>(
                nameof(this.GetDataSets),
                @param);
        }

        public IEnumerable<DataTypeRecord> GetDataTypes()
        {
            return this.session.Query<DataTypeRecord>(
                nameof(this.GetDataTypes));
        }

        public FeatureTypeRecord GetFeatureType(int id)
        {
            var @param = new { Id = id };
            return this.session.QuerySingle<FeatureTypeRecord>(
                nameof(this.GetFeatureType),
                @param);
        }

        public IEnumerable<FeatureTypeRecord> GetFeatureTypes(int clientId, int top)
        {
            var @param = new { ClientId = clientId, Top = top };
            return this.session.Query<FeatureTypeRecord>(
                nameof(this.GetFeatureTypes),
                @param);
        }

        public PlotRecord GetPlot(int plotId)
        {
            var @param = new { PlotId = plotId };
            return this.session.QuerySingle<PlotRecord>(
                nameof(this.GetPlot),
                @param);
        }

        public IEnumerable<PlotRecord> GetPlots(int clientId, int top)
        {
            var @param = new { ClientId = clientId, Top = top };
            return this.session.Query<PlotRecord>(
                nameof(this.GetPlots),
                @param);
        }

        public IEnumerable<PlotRecord> GetPlotsContainingGeometry(
            int clientId,
            string wkt,
            int srid)
        {
            var geometry = ValidSqlGeometryFromWkt(wkt, srid);
            var @param = new { ClientId = clientId, Geometry = geometry };
            return this.session.Query<PlotRecord>(
                nameof(this.GetPlotsContainingGeometry),
                @param);
        }

        public IEnumerable<StyleClassRecord> GetStyleClasses(int styleId)
        {
            var @param = new { StyleId = styleId };
            return this.session.Query<StyleClassRecord>(
                nameof(this.GetStyleClasses),
                @param);
        }

        public IEnumerable<StyleRecord> GetStyles(int featureTypeId)
        {
            var @param = new { FeatureTypeId = featureTypeId };
            return this.session.Query<StyleRecord>(
                nameof(this.GetStyles),
                @param);
        }

        public IEnumerable<StyleTypeRecord> GetStyleTypes()
        {
            return this.session.Query<StyleTypeRecord>(
                nameof(this.GetStyleTypes));
        }

        public int InsertClient(ClientRecord rec)
        {
            var @param = new { rec.Name };
            return this.session.Insert(
                nameof(this.InsertClient),
                @param);
        }

        public int InsertDataSet(DataSetRecord rec)
        {
            var @param = new { rec.PlotId, rec.FeatureTypeId, rec.Name, rec.DateCreated };
            return this.session.Insert(
                nameof(this.InsertDataSet),
                @param);
        }

        public int InsertDataType(DataTypeRecord rec)
        {
            var @param = new { rec.Name, rec.SqlType, rec.BclType };
            return this.session.Insert(
                nameof(this.InsertDataType),
                @param);
        }

        public int InsertFeatureType(FeatureTypeRecord rec)
        {
            var @param = new { rec.ClientId, rec.Name };
            return this.session.Insert(
                nameof(this.InsertFeatureType),
                @param);
        }

        public int InsertPlot(PlotRecord rec)
        {
            var geom = ValidSqlGeometryFromWkt(rec.Wkt, rec.SRID);
            var geog = ValidSqlGeographyFromWkt(rec.Wkt, rec.SRID);
            var @param = new
            {
                rec.ClientId,
                rec.Name,
                Geometry = geom,
                Geography = geog,
                rec.SRID,
            };

            return this.session.Insert(
                nameof(this.InsertPlot),
                @param);
        }

        public int InsertStyle(StyleRecord rec)
        {
            var @param = new
            {
                rec.FeatureTypeId,
                rec.AttributeIndex,
                rec.StyleTypeId,
                rec.Name,
            };

            return this.session.Insert(
                nameof(this.InsertStyle),
                @param);
        }

        public int InsertStyleClass(StyleClassRecord rec)
        {
            var @param = new
            {
                rec.StyleId,
                rec.Legend,
                rec.Category,
                rec.MinValue,
                rec.MaxValue,
            };

            return this.session.Insert(
                nameof(this.InsertStyleClass),
                @param);
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
                ["Geography"] = typeof(SqlGeography),
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
            return SqlGeometry.STGeomFromText(text, srid)
                .MakeValid();
        }

        private static SqlGeography ValidSqlGeographyFromWkt(string wkt, int srid)
        {
            var text = new SqlChars(new SqlString(wkt));

            // http://stackoverflow.com/questions/4409922/sql-spatial-polygon-inside-out
            // NOTE: We still have issues with some particular shapefiles and SqlGeography
            return SqlGeography.STGeomFromText(text, srid)
                .MakeValid()
                .ReorientObject();
        }
    }
}
