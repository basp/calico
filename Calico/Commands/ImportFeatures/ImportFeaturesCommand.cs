// <copyright file="ImportFeaturesCommand.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using System.Linq;
    using Optional;
    using Optional.Linq;
    using Serilog;

    using static Optional.Option;

    using Req = ImportFeaturesRequest;
    using Res = ImportFeaturesResponse;

    public class ImportFeaturesCommand : ICommand<Req, Res, Exception>
    {
        private readonly IRepository repository;
        private readonly Func<Option<IFeatureCollection, Exception>> featureCollectionProvider;

        public ImportFeaturesCommand(
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
                var dataSet = this.repository.GetDataSet(req.DataSetId);
                var featureType = this.repository.GetFeatureType(dataSet.FeatureTypeId);
                Log.Information(
                    "Importing features for data set {DataSetName} with feature type {FeatureTypeName}",
                    dataSet.Name,
                    featureType.Name);

                return from features in this.featureCollectionProvider()
                       let recs = features.Features.Select((x, i) => CreateFeatureRecord(req.DataSetId, i, x.Wkt, req.SRID))
                       let count = this.repository.BulkCopyFeatures(recs)
                       select new Res { RowCount = count };
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
