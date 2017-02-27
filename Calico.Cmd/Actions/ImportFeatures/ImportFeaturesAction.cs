namespace Calico.Cmd
{
    using System;
    using System.Data.SqlClient;
    using AutoMapper;

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
                    });

                    res.MatchNone(x =>
                    {
                        tx.Rollback();
                    });
                }
            }
        }
    }
}
