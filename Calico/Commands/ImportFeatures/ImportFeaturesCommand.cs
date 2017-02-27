namespace Calico
{
    using System;
    using System.Linq;
    using DotSpatial.Data;
    using Optional;

    using static Optional.Option;

    using Req = ImportFeaturesRequest;
    using Res = ImportFeaturesResponse;
    using Microsoft.SqlServer.Types;
    using DotSpatial.Topology;
    using System.Data.SqlTypes;

    public class ImportFeaturesCommand : ICommand<Req, Res, Exception>
    {
        private readonly IRepository repository;

        public ImportFeaturesCommand(IRepository repository)
        {
            this.repository = repository;
        }

        public Option<Res, Exception> Execute(Req req)
        {
            try
            {
                var shapefile = Shapefile.OpenFile(req.PathToShapefile);
                var recs = shapefile.Features
                    .Select((x, i) => CreateFeatureRecord(req.DataSetId, i, x, req.SRID));

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
            IFeature feature,
            int srid)
        {
            return new FeatureRecord
            {
                DataSetId = dataSetId,
                Index = index,
                Geometry = GetSTGeometry(feature.BasicGeometry, srid),
            };
        }

        private static SqlGeometry GetSTGeometry(IBasicGeometry bg, int srid)
        {
            var wkt = bg.ToString();
            var chars = new SqlChars(new SqlString(wkt));
            return SqlGeometry.STGeomFromText(chars, srid);
        }
    }
}
