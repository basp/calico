namespace Calico
{
    using System;
    using System.Data.SqlTypes;
    using DotSpatial.Data;
    using Microsoft.SqlServer.Types;
    using Optional;

    using static Optional.Option;

    using Req = NewPlotRequest;
    using Res = NewPlotResponse;

    public class NewPlotCommand : ICommand<Req, Res, Exception>
    {
        private readonly IRepository repository;

        public NewPlotCommand(IRepository repository)
        {
            this.repository = repository;
        }

        public Option<Res, Exception> Execute(Req req)
        {
            try
            {
                var shapefile = Shapefile.OpenFile(req.PathToShapefile);
                var feature = shapefile.GetFeature(0);
                var wkt = feature.BasicGeometry.ToString();
                var chars = new SqlChars(new SqlString(wkt));
                var geometry = SqlGeometry.STGeomFromText(chars, req.SRID);
                var rec = this.InsertPlot(req.ClientId, req.Name, geometry);
                var res = new Res { Plot = rec };
                return Some<Res, Exception>(res);
            }
            catch (Exception ex)
            {
                return None<Res, Exception>(ex);
            }
        }

        private PlotRecord InsertPlot(int clientId, string name, SqlGeometry geometry)
        {
            var rec = new PlotRecord
            {
                ClientId = clientId,
                Name = name,
                Geometry = geometry,
            };

            rec.Id = this.repository.InsertPlot(rec);
            return rec;
        }
    }
}
