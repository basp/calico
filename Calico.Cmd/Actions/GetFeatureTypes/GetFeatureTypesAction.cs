namespace Calico.Cmd
{
    using System;
    using System.Data.SqlClient;
    using AutoMapper;
    using Newtonsoft.Json;
    using Serilog;

    public class GetFeatureTypesAction : IAction<GetFeatureTypesArgs>
    {
        private readonly Func<SqlConnection> connectionFactory;

        public GetFeatureTypesAction(Func<SqlConnection> connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public void Execute(GetFeatureTypesArgs args)
        {
            using (var conn = this.connectionFactory())
            {
                var repo = new SqlRepository(conn);
                var cmd = new GetFeatureTypesCommand(repo);
                var req = Mapper.Map<GetFeatureTypesRequest>(args);
                var res = cmd.Execute(req);

                res.MatchSome(x =>
                {
                    var json = JsonConvert.SerializeObject(x.FeatureTypes);
                    Console.WriteLine(json);
                });

                res.MatchNone(x =>
                {
                    Log.Error(x, "Could not get feature types");
                });
            }
        }
    }
}
