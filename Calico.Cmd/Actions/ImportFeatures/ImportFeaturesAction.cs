namespace Calico.Cmd
{
    using System;
    using System.Data.SqlClient;
    using AutoMapper;
    using Serilog;

    public class ImportFeaturesAction : IAction<ImportFeaturesArgs>
    {
        private readonly Func<SqlConnection> connectionFactory;

        public ImportFeaturesAction(Func<SqlConnection> connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public void Execute(ImportFeaturesArgs args)
        {
            using (var conn = this.connectionFactory())
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    var repo = new SqlRepository(conn, tx);
                    var cmd = new ImportFeaturesCommand(repo);
                    var req = Mapper.Map<ImportFeaturesRequest>(args);
                    var res = cmd.Execute(req);

                    res.MatchSome(x =>
                    {
                        tx.Commit();
                        Log.Information(
                            "Imported {FeatureCount} features from {Shapefile}",
                            x.RowCount,
                            req.PathToShapefile);
                    });

                    res.MatchNone(x =>
                    {
                        tx.Rollback();
                        Log.Error(
                            x,
                            "Could not import features from shapefile {Shapefile}",
                            req.PathToShapefile);
                    });
                }
            }
        }
    }
}
