namespace Calico.Cmd
{
    using AutoMapper;
    using Serilog;
    using System;
    using System.Data.SqlClient;

    public class ImportAttributeValuesAction : IAction<ImportAttributeValuesArgs>
    {
        private readonly Func<SqlConnection> connectionFactory;

        public ImportAttributeValuesAction(Func<SqlConnection> connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public void Execute(ImportAttributeValuesArgs args)
        {
            using (var conn = this.connectionFactory())
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    var repo = new SqlRepository(conn, tx);
                    var cmd = new ImportAttributeValuesCommand(repo);
                    var req = Mapper.Map<ImportAttributeValuesRequest>(args);
                    var res = cmd.Execute(req);

                    res.MatchSome(x =>
                    {
                        tx.Commit();
                        Log.Information(
                            "Imported {RowCount} values from {Shapefile} into data set {DataSetId}",
                            x.RowCount,
                            req.PathToShapefile,
                            req.DataSetId);
                    });

                    res.MatchNone(x =>
                    {
                        tx.Rollback();
                        Log.Error(
                            x,
                            "Failed to import attribute values from {Shapefile}",
                            req.PathToShapefile);
                    });
                }
            }
        }
    }
}
