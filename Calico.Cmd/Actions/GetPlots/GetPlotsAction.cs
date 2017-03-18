// <copyright file="GetPlotsAction.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

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
            using (var session = SqlSession.Open(conn))
            {
                var repo = new SqlRepository(session);
                var cmd = new GetPlotsCommand(repo);
                var req = Mapper.Map<GetPlotsRequest>(args);
                var res = cmd.Execute(req);

                res.MatchSome(x =>
                {
                    session.Commit();
                    var plots = x.Plots.Select(y => new
                    {
                        y.Id,
                        y.TenantId,
                        y.Name,
                        Geometry = y.Wkt,
                        y.SRID,
                    });

                    var json = JsonConvert.SerializeObject(plots);
                    Console.WriteLine(json);
                });

                res.MatchNone(x =>
                {
                    session.Rollback();
                    Log.Error(
                        x,
                        "Could not get plots for tenant {TenantId}",
                        args.TenantId);
                });
            }
        }
    }
}
