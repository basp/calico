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
    using Optional.Linq;
    using Serilog;

    using static Optional.Option;

    using Req = ImportAttributeValuesRequest;
    using Res = ImportAttributeValuesResponse;

    public class ImportAttributeValuesCommand : ICommand<Req, Res, Exception>
    {
        private readonly IRepository repository;
        private readonly Func<Option<IFeatureCollection, Exception>> featureCollectionProvider;

        public ImportAttributeValuesCommand(
            IRepository repository,
            Func<Option<IFeatureCollection, Exception>> featureCollectionProvider)
        {
            this.repository = repository;
            this.featureCollectionProvider = featureCollectionProvider;
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

                return from features in this.featureCollectionProvider()
                       let table = features.DataTable
                       let cols = table.Columns.Cast<DataColumn>()
                       let recs = this.GetAttributeValues(dataSet.Id, featureType.Id, table)
                       let count = this.repository.BulkCopyAttributeValues(recs)
                       select new Res { RowCount = count };
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
