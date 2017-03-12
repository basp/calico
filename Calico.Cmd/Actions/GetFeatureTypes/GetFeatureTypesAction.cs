// <copyright file="GetFeatureTypesAction.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

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
            using (var session = SqlSession.Open(conn))
            {
                var repo = new SqlRepository(session);
                var cmd = new GetFeatureTypesCommand(repo);
                var req = Mapper.Map<GetFeatureTypesRequest>(args);
                var res = cmd.Execute(req);

                res.MatchSome(x =>
                {
                    session.Commit();
                    var json = JsonConvert.SerializeObject(x.FeatureTypes);
                    Console.WriteLine(json);
                });

                res.MatchNone(x =>
                {
                    session.Rollback();
                    Log.Error(x, "Could not get feature types");
                });
            }
        }
    }
}
