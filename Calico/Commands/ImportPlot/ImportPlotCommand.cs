// <copyright file="ImportPlotCommand.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using Optional;
    using Optional.Linq;

    using static Optional.Option;

    using Req = ImportPlotRequest;
    using Res = ImportPlotResponse;

    public class ImportPlotCommand : ICommand<Req, Res, Exception>
    {
        private IRepository repository;

        public ImportPlotCommand(IRepository repository)
        {
            this.repository = repository;
        }

        public Option<Res, Exception> Execute(Req req)
        {
            var res = from plot in this.NewPlot(req)
                      from dataSet in this.NewDataSet(plot.Id, req.FeatureTypeId, req.Name)
                      from features in this.ImportFeatures(dataSet.Id, req.PathToShapefile, req.SRID)
                      from attrs in this.ImportAttributeValues(dataSet.Id, req.PathToShapefile)
                      select new Res
                      {
                          Plot = plot,
                          DataSet = dataSet,
                          NumberOfFeatures = features,
                          NumberOfAttributes = attrs,
                      };

            return res;
        }

        private Option<PlotRecord, Exception> NewPlot(Req req)
        {
            var cmd = new NewPlotCommand(this.repository);
            var res = cmd.Execute(new NewPlotRequest
            {
                ClientId = req.ClientId,
                Name = req.Name,
                PathToShapefile = req.PathToShapefile,
            });

            return res.Map(x => x.Plot);
        }

        private Option<DataSetRecord, Exception> NewDataSet(
            int plotId,
            int featureTypeId,
            string name)
        {
            var cmd = new NewDataSetCommand(this.repository);
            var res = cmd.Execute(new NewDataSetRequest
            {
                PlotId = plotId,
                FeatureTypeId = featureTypeId,
                Name = name,
                DateCreated = DateTime.UtcNow,
            });

            return res.Map(x => x.DataSet);
        }

        private Option<int, Exception> ImportFeatures(
            int dataSetId,
            string pathToShapefile,
            int srid)
        {
            var cmd = new ImportFeaturesCommand(this.repository);
            var res = cmd.Execute(new ImportFeaturesRequest
            {
                DataSetId = dataSetId,
                PathToShapefile = pathToShapefile,
                SRID = srid,
            });

            return res.Map(x => x.RowCount);
        }

        private Option<int, Exception> ImportAttributeValues(
            int dataSetId,
            string pathToShapefile)
        {
            var cmd = new ImportAttributeValuesCommand(this.repository);
            var res = cmd.Execute(new ImportAttributeValuesRequest
            {
                DataSetId = dataSetId,
                PathToShapefile = pathToShapefile,
            });

            return res.Map(x => x.RowCount);
        }
    }
}
