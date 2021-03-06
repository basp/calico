﻿// <copyright file="ImportDataSetCommand.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using Optional;
    using Optional.Linq;

    using Req = ImportDataSetRequest;
    using Res = ImportDataSetResponse;

    public class ImportDataSetCommand : ICommand<Req, Res, Exception>
    {
        private readonly IRepository repository;
        private readonly Func<Option<IFeatureCollection, Exception>> featureCollectionProvider;

        public ImportDataSetCommand(
            IRepository repository,
            Func<Option<IFeatureCollection, Exception>> featureCollectionProvider)
        {
            this.repository = repository;
            this.featureCollectionProvider = featureCollectionProvider;
        }

        public Option<Res, Exception> Execute(Req req)
        {
            return from dataSet in this.NewDataSet(req)
                   from features in this.ImportFeatures(dataSet.Id, req)
                   from attrs in this.ImportAttributeValues(dataSet.Id)
                   let plot = this.repository.GetPlot(req.PlotId)
                   select new Res
                   {
                       Plot = plot,
                       DataSet = dataSet,
                       NumberOfFeatures = features,
                       NumberOfAttributes = attrs,
                   };
        }

        private Option<DataSetRecord, Exception> NewDataSet(Req req)
        {
            var cmd = new NewDataSetCommand(this.repository);
            var res = cmd.Execute(new NewDataSetRequest
            {
                PlotId = req.PlotId,
                FeatureTypeId = req.FeatureTypeId,
                Name = req.Name,
                DateCreated = req.DateCreated,
            });

            return res.Map(x => x.DataSet);
        }

        private Option<int, Exception> ImportFeatures(int dataSetId, Req req)
        {
            var cmd = new ImportFeaturesCommand(this.repository, this.featureCollectionProvider);
            var res = cmd.Execute(new ImportFeaturesRequest
            {
                DataSetId = dataSetId,
                SRID = req.SRID,
            });

            return res.Map(x => x.RowCount);
        }

        private Option<int, Exception> ImportAttributeValues(int dataSetId)
        {
            var cmd = new ImportAttributeValuesCommand(this.repository, this.featureCollectionProvider);
            var res = cmd.Execute(new ImportAttributeValuesRequest
            {
                DataSetId = dataSetId,
            });

            return res.Map(x => x.RowCount);
        }
    }
}
