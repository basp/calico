// <copyright file="ImportAttributesCommand.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using System.Data;
    using System.Linq;
    using Optional;
    using Optional.Linq;
    using Serilog;

    using static Optional.Option;

    using Req = ImportAttributesRequest;
    using Res = ImportAttributesResponse;

    public class ImportAttributesCommand : ICommand<Req, Res, Exception>
    {
        private readonly IRepository repository;
        private readonly Func<Option<IFeatureCollection, Exception>> featureCollectionProvider;

        public ImportAttributesCommand(
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
                var featureType = this.repository.GetFeatureType(req.FeatureTypeId);
                Log.Information(
                    "Importing attributes for feature type {FeatureTypeName}",
                    featureType.Name);

                var dataTypes = this.repository
                    .GetDataTypes()
                    .ToDictionary(x => x.BclType, x => x);

                return from features in this.featureCollectionProvider()
                       let table = features.DataTable
                       let cols = table.Columns.Cast<DataColumn>()
                       let recs = cols.Select((x, i) => new AttributeRecord
                       {
                           FeatureTypeId = req.FeatureTypeId,
                           DataTypeId = dataTypes[x.DataType.FullName].Id,
                           Index = i,
                           Name = x.ColumnName,
                       })
                       let c = this.repository.BulkCopyAttributes(recs)
                       select new Res { Attributes = recs };
            }
            catch (Exception ex)
            {
                return None<Res, Exception>(ex);
            }
        }
    }
}
