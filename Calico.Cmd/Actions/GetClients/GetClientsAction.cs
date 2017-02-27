namespace Calico.Cmd
{
    using System;
    using System.Data.SqlClient;
    using AutoMapper;
    using Serilog;
    using Newtonsoft.Json;

    public class GetClientsAction : IAction<GetClientsArgs>
    {
        private readonly Func<SqlConnection> connectionFactory;

        public GetClientsAction(Func<SqlConnection> connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public void Execute(GetClientsArgs args)
        {
            using (var conn = this.connectionFactory())
            {
                var repo = new SqlRepository(conn);
                var cmd = new GetClientsCommand(repo);
                var req = Mapper.Map<GetClientsRequest>(args);
                var res = cmd.Execute(req);

                res.MatchSome(x =>
                {
                    var json = JsonConvert.SerializeObject(x.Clients);
                    Console.WriteLine(json);
                });

                res.MatchNone(x =>
                {
                    Log.Error(x, "Could not get clients");
                });
            }
        }
    }
}
