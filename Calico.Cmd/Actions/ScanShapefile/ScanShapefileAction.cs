namespace Calico.Cmd
{
    using AutoMapper;
    using Newtonsoft.Json;
    using Serilog;
    using System;
    using System.Data.SqlClient;

    public class ScanShapefileAction : IAction<ScanShapefileArgs>
    {
        private readonly Func<SqlConnection> connectionFactory;

        public ScanShapefileAction(Func<SqlConnection> connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public void Execute(ScanShapefileArgs args)
        {
            using (var conn = this.connectionFactory())
            {
                var repo = new SqlRepository(conn);
                var cmd = new ScanShapefileCommand(repo);
                var req = Mapper.Map<ScanShapefileRequest>(args);
                var res = cmd.Execute(req);

                res.MatchSome(x =>
                {
                    var json = JsonConvert.SerializeObject(x);
                    Console.WriteLine(json);
                });

                res.MatchNone(x =>
                {
                    Log.Error(x, "Could not scan shapefile {Shapefile}", req.PathToShapefile);
                });
            }
        }
    }
}
