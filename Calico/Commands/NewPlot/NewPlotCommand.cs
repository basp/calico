// <copyright file="NewPlotCommand.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using System.Linq;
    using Optional;

    using static Optional.Option;

    using Req = NewPlotRequest;
    using Res = NewPlotResponse;

    public class NewPlotCommand : ICommand<Req, Res, Exception>
    {
        private readonly IRepository repository;
        private readonly IFeatureCollection featureCollection;

        public NewPlotCommand(
            IRepository repository,
            IFeatureCollection featureCollection)
        {
            this.repository = repository;
            this.featureCollection = featureCollection;
        }

        public Option<Res, Exception> Execute(Req req)
        {
            try
            {
                var feature = this.featureCollection.GetFeatures().First();
                var rec = this.InsertPlot(req.ClientId, req.Name, feature.Wkt, req.SRID);
                var res = new Res { Plot = rec };
                return Some<Res, Exception>(res);
            }
            catch (Exception ex)
            {
                return None<Res, Exception>(ex);
            }
        }

        private PlotRecord InsertPlot(int clientId, string name, string wkt, int srid)
        {
            var rec = new PlotRecord
            {
                ClientId = clientId,
                Name = name,
                Wkt = wkt,
                SRID = srid,
            };

            rec.Id = this.repository.InsertPlot(rec);
            return rec;
        }
    }
}
