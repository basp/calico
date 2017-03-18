// <copyright file="ImportPlotCommand.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using Optional;
    using Optional.Linq;

    using Req = ImportPlotRequest;
    using Res = ImportPlotResponse;

    public class ImportPlotCommand : ICommand<Req, Res, Exception>
    {
        private IRepository repository;
        private Func<Option<IFeatureCollection, Exception>> featureCollectionProvider;

        public ImportPlotCommand(
            IRepository repository,
            Func<Option<IFeatureCollection, Exception>> featureCollectionProvider)
        {
            this.repository = repository;
            this.featureCollectionProvider = featureCollectionProvider;
        }

        public Option<Res, Exception> Execute(Req req)
        {
            return from plot in this.NewPlot(req)
                   from dataSet in this.NewDataSet(plot.Id, req)
                   from features in this.ImportFeatures(dataSet.Id, req.SRID)
                   from attrs in this.ImportAttributeValues(dataSet.Id)
                   select new Res
                   {
                       Plot = plot,
                       DataSet = dataSet,
                       NumberOfFeatures = features,
                       NumberOfAttributes = attrs,
                   };
        }

        private Option<PlotRecord, Exception> NewPlot(Req req)
        {
            var cmd = new NewPlotCommand(this.repository, this.featureCollectionProvider);
            var res = cmd.Execute(new NewPlotRequest
            {
                TenantId = req.TenantId,
                Name = req.Name,
                SRID = req.SRID,
            });

            return res.Map(x => x.Plot);
        }

        private Option<DataSetRecord, Exception> NewDataSet(
            int plotId,
            Req req)
        {
            var cmd = new NewDataSetCommand(this.repository);
            var res = cmd.Execute(new NewDataSetRequest
            {
                PlotId = plotId,
                FeatureTypeId = req.FeatureTypeId,
                Name = req.Name,
                DateCreated = req.DateCreated,
            });

            return res.Map(x => x.DataSet);
        }

        private Option<int, Exception> ImportFeatures(
            int dataSetId,
            int srid)
        {
            var cmd = new ImportFeaturesCommand(this.repository, this.featureCollectionProvider);
            var res = cmd.Execute(new ImportFeaturesRequest
            {
                DataSetId = dataSetId,
                SRID = srid,
            });

            return res.Map(x => x.RowCount);
        }

        private Option<int, Exception> ImportAttributeValues(
            int dataSetId)
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
