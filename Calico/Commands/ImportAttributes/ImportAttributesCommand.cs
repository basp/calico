// <copyright file="ImportAttributesCommand.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using System.Data;
    using System.Linq;
    using Optional;
    using Serilog;

    using static Optional.Option;

    using Req = ImportAttributesRequest;
    using Res = ImportAttributesResponse;

    public class ImportAttributesCommand : ICommand<Req, Res, Exception>
    {
        private readonly IRepository repository;
        private readonly IFeatureCollection featureCollection;

        public ImportAttributesCommand(IRepository repository, IFeatureCollection featureCollection)
        {
            this.repository = repository;
            this.featureCollection = featureCollection;
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

                var table = this.featureCollection.GetDataTable();
                var recs = table.Columns
                    .Cast<DataColumn>()
                    .Select((x, i) => new AttributeRecord
                    {
                        FeatureTypeId = req.FeatureTypeId,
                        DataTypeId = dataTypes[x.DataType.FullName].Id,
                        Index = i,
                        Name = x.ColumnName,
                    });

                var c = this.repository.BulkCopyAttributes(recs);
                var res = new Res { Attributes = recs };
                return Some<Res, Exception>(res);
            }
            catch (Exception ex)
            {
                return None<Res, Exception>(ex);
            }
        }
    }
}
