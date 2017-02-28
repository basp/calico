// <copyright file="ImportAttributeValuesCommand.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using DotSpatial.Data;
    using Optional;
    using Serilog;

    using static Optional.Option;

    using Req = ImportAttributeValuesRequest;
    using Res = ImportAttributeValuesResponse;

    public class ImportAttributeValuesCommand : ICommand<Req, Res, Exception>
    {
        private readonly IRepository repository;

        public ImportAttributeValuesCommand(IRepository repository)
        {
            this.repository = repository;
        }

        public Option<Res, Exception> Execute(Req req)
        {
            try
            {
                var shapefile = Shapefile.OpenFile(req.PathToShapefile);
                var dataTypes = this.repository.GetDataTypes()
                    .ToDictionary(x => x.Id, x => x);

                var dataSet = this.repository.GetDataSet(req.DataSetId);
                var featureType = this.repository.GetFeatureType(dataSet.FeatureTypeId);

                Log.Information(
                    "Importing data for data set {DataSetName} with feature type {FeatureTypeName}",
                    dataSet.Name,
                    featureType.Name);

                var attributes = this.repository.GetAttributes(dataSet.FeatureTypeId);
                foreach (var attr in attributes)
                {
                    var dataType = dataTypes[attr.DataTypeId];
                    Log.Debug(
                        "Attribute {AttributeName} ({DataTypeName})",
                        attr.Name,
                        dataType.Name);
                }

                var mapping = attributes.ToDictionary(x => x.Name, x => x);

                foreach (var col in shapefile.GetColumns())
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
                    shapefile.DataTable);

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

                    switch (dt.Name)
                    {
                        case "Long":
                            rec.LongValue = (long)table.Rows[i][j];
                            break;
                        case "Double":
                            rec.DoubleValue = (double)table.Rows[i][j];
                            break;
                        case "String":
                            rec.StringValue = table.Rows[i][j].ToString();
                            break;
                    }

                    yield return rec;
                }
            }
        }
    }
}
