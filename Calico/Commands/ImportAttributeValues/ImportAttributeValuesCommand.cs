// <copyright file="ImportAttributeValuesCommand.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Optional;
    using Serilog;

    using static Optional.Option;

    using Req = ImportAttributeValuesRequest;
    using Res = ImportAttributeValuesResponse;

    public class ImportAttributeValuesCommand : ICommand<Req, Res, Exception>
    {
        private readonly IRepository repository;
        private readonly IFeatureCollection features;

        public ImportAttributeValuesCommand(
            IRepository repository,
            IFeatureCollection features)
        {
            this.repository = repository;
            this.features = features;
        }

        public Option<Res, Exception> Execute(Req req)
        {
            try
            {
                var dataTypes = this.repository.GetDataTypes()
                    .ToDictionary(x => x.Id, x => x);

                var dataSet = this.repository.GetDataSet(req.DataSetId);
                var featureType = this.repository.GetFeatureType(dataSet.FeatureTypeId);

                Log.Information(
                    "Importing data for data set {DataSetName} with feature type {FeatureTypeName}",
                    dataSet.Name,
                    featureType.Name);

                var attributes = this.repository.GetAttributes(dataSet.FeatureTypeId);
                var mapping = attributes.ToDictionary(x => x.Name, x => x);

                var table = this.features.GetDataTable();
                var columns = table.Columns.Cast<DataColumn>();
                foreach (var col in columns)
                {
                    if (!mapping.ContainsKey(col.ColumnName))
                    {
                        var msg = string.Format(
                            "Column {0} is not suppored by feature type {1}",
                            col.ColumnName,
                            featureType.Name);

                        return None<Res, Exception>(new Exception(msg));
                    }
                }

                var recs = this.GetAttributeValues(
                    dataSet.Id,
                    featureType.Id,
                    table);

                var c = this.repository.BulkCopyAttributeValues(recs);
                var res = new Res { RowCount = c };
                return Some<Res, Exception>(res);
            }
            catch (Exception ex)
            {
                return None<Res, Exception>(ex);
            }
        }

        private IEnumerable<AttributeValueRecord> GetAttributeValues(
            int dataSetId,
            int featureTypeId,
            DataTable table)
        {
            var attributes = this.repository
                .GetAttributes(featureTypeId)
                .ToArray();

            var dataTypes = this.repository
                .GetDataTypes()
                .ToDictionary(x => x.Id, x => x);

            for (var i = 0; i < table.Rows.Count; i++)
            {
                for (var j = 0; j < table.Columns.Count; j++)
                {
                    var rec = new AttributeValueRecord
                    {
                        DataSetId = dataSetId,
                        FeatureIndex = i,
                        AttributeIndex = j,
                    };

                    var attr = attributes[j];
                    var dt = dataTypes[attr.DataTypeId];

                    var val = table.Rows[i][j];

                    switch (dt.Name)
                    {
                        case "Long":
                            rec.LongValue = val == DBNull.Value ? (long?)null : (long)val;
                            break;
                        case "Double":
                            rec.DoubleValue = val == DBNull.Value ? (double?)null : (double)val;
                            break;
                        case "String":
                            rec.StringValue = val == DBNull.Value ? null : (string)val;
                            break;
                    }

                    yield return rec;
                }
            }
        }
    }
}
