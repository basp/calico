// <copyright file="ImportFeaturesCommand.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using System.Linq;
    using Optional;
    using Serilog;

    using static Optional.Option;

    using Req = ImportFeaturesRequest;
    using Res = ImportFeaturesResponse;

    public class ImportFeaturesCommand : ICommand<Req, Res, Exception>
    {
        private readonly IRepository repository;
        private readonly IFeatureCollection features;

        public ImportFeaturesCommand(IRepository repository, IFeatureCollection features)
        {
            this.repository = repository;
            this.features = features;
        }

        public Option<Res, Exception> Execute(Req req)
        {
            try
            {
                var dataSet = this.repository.GetDataSet(req.DataSetId);
                var featureType = this.repository.GetFeatureType(dataSet.FeatureTypeId);
                Log.Information(
                    "Importing features for data set {DataSetName} with feature type {FeatureTypeName}",
                    dataSet.Name,
                    featureType.Name);

                var recs = this.features
                    .GetFeatures()
                    .Select((x, i) => CreateFeatureRecord(req.DataSetId, i, x.Wkt, req.SRID));

                var c = this.repository.BulkCopyFeatures(recs);
                var res = new Res { RowCount = c };
                return Some<Res, Exception>(res);
            }
            catch (Exception ex)
            {
                return None<Res, Exception>(ex);
            }
        }

        private static FeatureRecord CreateFeatureRecord(
            int dataSetId,
            int index,
            string wkt,
            int srid)
        {
            return new FeatureRecord
            {
                DataSetId = dataSetId,
                Index = index,
                Wkt = wkt,
                SRID = srid,
            };
        }
    }
}
