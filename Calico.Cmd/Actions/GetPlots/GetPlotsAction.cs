namespace Calico.Cmd
{
    using System;
    using System.Data.SqlClient;
    using System.Linq;
    using AutoMapper;
    using Newtonsoft.Json;
    using Serilog;

    public class GetPlotsAction : IAction<GetPlotsArgs>
    {
        private readonly Func<SqlConnection> connectionFactory;

        public GetPlotsAction(Func<SqlConnection> connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public void Execute(GetPlotsArgs args)
        {
            using (var conn = this.connectionFactory())
            {
                var repo = new SqlRepository(conn);
                var cmd = new GetPlotsCommand(repo);
                var req = Mapper.Map<GetPlotsRequest>(args);
                var res = cmd.Execute(req);

                res.MatchSome(x =>
                {
                    var plots = x.Plots.Select(y => new
                    {
                        y.Id,
                        y.ClientId,
                        y.Name,
                        Geometry = y.Geometry.ToString(),
                    });

                    var json = JsonConvert.SerializeObject(plots);
                    Console.WriteLine(json);
                });

                res.MatchNone(x =>
                {
                    Log.Error(
                        x,
                        "Could not get plots for client {ClientId}",
                        args.ClientId);
                });
            }
        }
    }
}
