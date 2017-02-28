namespace Calico.Cmd
{
    using AutoMapper;
    using Serilog;
    using System;
    using System.Data.SqlClient;

    public class ImportAttributesAction : IAction<ImportAttributesArgs>
    {
        private readonly Func<SqlConnection> connectionFactory;

        public ImportAttributesAction(Func<SqlConnection> connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public void Execute(ImportAttributesArgs args)
        {
            using (var conn = this.connectionFactory())
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    var repo = new SqlRepository(conn, tx);
                    var cmd = new ImportAttributesCommand(repo);
                    var req = Mapper.Map<ImportAttributesRequest>(args);
                    var res = cmd.Execute(req);

                    res.MatchSome(x =>
                    {
                        tx.Commit();
                        Log.Information(
                            "Imported {RowCount} attributes from {Shapefile}",
                            x.RowCount,
                            req.PathToShapefile);
                    });

                    res.MatchNone(x =>
                    {
                        tx.Rollback();
                        Log.Error(
                            x,
                            "Failed to import attributes from {Shapefile}",
                            req.PathToShapefile);
                    });
                }
            }
        }
    }
}
