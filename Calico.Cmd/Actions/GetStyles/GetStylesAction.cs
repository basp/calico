// <copyright file="GetStylesAction.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using System;
    using System.Data.SqlClient;
    using AutoMapper;
    using Newtonsoft.Json;
    using Serilog;

    public class GetStylesAction : IAction<GetStylesArgs>
    {
        private readonly Func<SqlConnection> connectionFactory;

        public GetStylesAction(Func<SqlConnection> connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public void Execute(GetStylesArgs args)
        {
            using (var conn = this.connectionFactory())
            using (var session = SqlSession.Open(conn))
            {
                var repo = new SqlRepository(session);
                var cmd = new GetStylesCommand(repo);
                var req = Mapper.Map<GetStylesRequest>(args);
                var res = cmd.Execute(req);

                res.MatchSome(x =>
                {
                    session.Commit();
                    var json = JsonConvert.SerializeObject(x.Styles);
                    Console.WriteLine(json);
                });

                res.MatchNone(x =>
                {
                    session.Rollback();
                    Log.Error(
                        x,
                        "Could not get styles for feature type {FeatureTypeId}",
                        args.FeatureTypeId);
                });
            }
        }
    }
}
