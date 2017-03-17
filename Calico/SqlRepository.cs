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
    using Microsoft.SqlServer.Types;

    public class SqlRepository : IRepository
    {
        private readonly ISession session;

        public SqlRepository(ISession session)
        {
            this.session = session;
        }

        public static SqlRepository Create(SqlConnection conn)
        {
            return new SqlRepository(SqlSession.Open(conn));
        }

        public virtual int BulkCopyAttributes(IEnumerable<AttributeRecord> recs)
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

        public virtual int BulkCopyAttributeValues(IEnumerable<AttributeValueRecord> recs)
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

        public virtual int BulkCopyFeatures(IEnumerable<FeatureRecord> recs)
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

        public virtual int DeleteDataSet(int id)
        {
            var @param = new { Id = id };
            return this.session.Execute(
                nameof(this.DeleteDataSet),
                @param);
        }

        public virtual int DeleteFeatureType(int id)
        {
            var @param = new { Id = id };
            return this.session.Execute(
                nameof(this.DeleteFeatureType),
                @param);
        }

        public virtual int DeletePlot(int id)
        {
            var @param = new { Id = id };
            return this.session.Execute(
                nameof(this.DeletePlot),
                @param);
        }

        public virtual IEnumerable<AttributeRecord> GetAttributes(int featureTypeId)
        {
            var @param = new { FeatureTypeId = featureTypeId };
            return this.session.Query<AttributeRecord>(
                nameof(this.GetAttributes),
                @param);
        }

        public virtual IEnumerable<AttributeValueRecord> GetAttributeValues(int dataSetId)
        {
            var @param = new { DataSetId = dataSetId };
            return this.session.Query<AttributeValueRecord>(
                nameof(this.GetAttributeValues),
                @param);
        }

        public virtual ClientRecord GetClient(int id)
        {
            var @param = new { Id = id };
            return this.session.QuerySingle<ClientRecord>(
                nameof(this.GetClient),
                @param);
        }

        public virtual IEnumerable<ClientRecord> GetClients(int first)
        {
            var @param = new { Top = first };
            return this.session.Query<ClientRecord>(
                nameof(this.GetClients),
                @param);
        }

        public virtual DataSetRecord GetDataSet(int id)
        {
            var @param = new { Id = id };
            return this.session.QuerySingle<DataSetRecord>(
                nameof(this.GetDataSet),
                @param);
        }

        public virtual IEnumerable<DataSetRecord> GetDataSets(int plotId, int first)
        {
            var @param = new { PlotId = plotId, Top = first };
            return this.session.Query<DataSetRecord>(
                nameof(this.GetDataSets),
                @param);
        }

        public virtual DataTypeRecord GetDataType(int id)
        {
            var @param = new { Id = id };
            return this.session.QuerySingle<DataTypeRecord>(
                nameof(this.GetDataType),
                @param);
        }

        public virtual IEnumerable<DataTypeRecord> GetDataTypes()
        {
            return this.session.Query<DataTypeRecord>(
                nameof(this.GetDataTypes));
        }

        public virtual IEnumerable<FeatureRecord> GetFeatures(int dataSetId)
        {
            var @param = new { DataSetId = dataSetId };
            return this.session.Query<FeatureRecord>(
                nameof(this.GetFeatures),
                @param);
        }

        public virtual FeatureTypeRecord GetFeatureType(int id)
        {
            var @param = new { Id = id };
            return this.session.QuerySingle<FeatureTypeRecord>(
                nameof(this.GetFeatureType),
                @param);
        }

        public virtual IEnumerable<FeatureTypeRecord> GetFeatureTypes(int clientId, int first)
        {
            var @param = new { ClientId = clientId, Top = first };
            return this.session.Query<FeatureTypeRecord>(
                nameof(this.GetFeatureTypes),
                @param);
        }

        public virtual PlotRecord GetPlot(int plotId)
        {
            var @param = new { PlotId = plotId };
            return this.session.QuerySingle<PlotRecord>(
                nameof(this.GetPlot),
                @param);
        }

        public virtual IEnumerable<PlotRecord> GetPlots(int clientId, int first)
        {
            var @param = new { ClientId = clientId, Top = first };
            return this.session.Query<PlotRecord>(
                nameof(this.GetPlots),
                @param);
        }

        public virtual IEnumerable<PlotRecord> GetPlotsContainingGeometry(
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

        public virtual IEnumerable<StyleClassRecord> GetStyleClasses(int styleId)
        {
            var @param = new { StyleId = styleId };
            return this.session.Query<StyleClassRecord>(
                nameof(this.GetStyleClasses),
                @param);
        }

        public virtual IEnumerable<StyleRecord> GetStyles(int featureTypeId)
        {
            var @param = new { FeatureTypeId = featureTypeId };
            return this.session.Query<StyleRecord>(
                nameof(this.GetStyles),
                @param);
        }

        public virtual IEnumerable<StyleTypeRecord> GetStyleTypes()
        {
            return this.session.Query<StyleTypeRecord>(
                nameof(this.GetStyleTypes));
        }

        public virtual int InsertClient(ClientRecord rec)
        {
            var @param = new { rec.Name };
            return this.session.Insert(
                nameof(this.InsertClient),
                @param);
        }

        public virtual int InsertDataSet(DataSetRecord rec)
        {
            var @param = new { rec.PlotId, rec.FeatureTypeId, rec.Name, rec.DateCreated };
            return this.session.Insert(
                nameof(this.InsertDataSet),
                @param);
        }

        public virtual int InsertDataType(DataTypeRecord rec)
        {
            var @param = new { rec.Name, rec.SqlType, rec.BclType };
            return this.session.Insert(
                nameof(this.InsertDataType),
                @param);
        }

        public virtual int InsertFeatureType(FeatureTypeRecord rec)
        {
            var @param = new { rec.ClientId, rec.Name };
            return this.session.Insert(
                nameof(this.InsertFeatureType),
                @param);
        }

        public virtual int InsertPlot(PlotRecord rec)
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

        public virtual int InsertStyle(StyleRecord rec)
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

        public virtual int InsertStyleClass(StyleClassRecord rec)
        {
            var @param = new
            {
                rec.StyleId,
                rec.Symbol,
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
