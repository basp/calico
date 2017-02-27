namespace Calico.Cmd
{
    using AutoMapper;
    using System;
    using System.Data.SqlClient;

    public class NewClientAction : IAction<NewClientArgs>
    {
        private readonly Func<SqlConnection> connectionFactory;

        public NewClientAction(Func<SqlConnection> connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public void Execute(NewClientArgs args)
        {
            using (var conn = this.connectionFactory())
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    var repo = new SqlRepository(conn, tx);
                    var cmd = new NewClientCommand(repo);
                    var req = Mapper.Map<NewClientRequest>(args);
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
