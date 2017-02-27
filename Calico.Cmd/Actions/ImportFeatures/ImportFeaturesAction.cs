namespace Calico.Cmd
{
    using System;
    using System.Data.SqlClient;

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

                    throw new NotImplementedException();
                }
            }
        }
    }
}
