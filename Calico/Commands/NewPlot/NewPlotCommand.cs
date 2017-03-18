// <copyright file="NewPlotCommand.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using System.Linq;
    using Optional;
    using Optional.Linq;

    using static Optional.Option;

    using Req = NewPlotRequest;
    using Res = NewPlotResponse;

    public class NewPlotCommand : ICommand<Req, Res, Exception>
    {
        private readonly IRepository repository;
        private readonly Func<Option<IFeatureCollection, Exception>> featureCollectionProvider;

        public NewPlotCommand(
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
                return from features in this.featureCollectionProvider()
                       let first = features.Features.First()
                       let rec = this.InsertPlot(req.TenantId, req.Name, first.Wkt, req.SRID)
                       select new Res { Plot = rec };
            }
            catch (Exception ex)
            {
                return None<Res, Exception>(ex);
            }
        }

        private PlotRecord InsertPlot(int tenantId, string name, string wkt, int srid)
        {
            var rec = new PlotRecord
            {
                TenantId = tenantId,
                Name = name,
                Wkt = wkt,
                SRID = srid,
            };

            rec.Id = this.repository.InsertPlot(rec);
            return rec;
        }
    }
}
