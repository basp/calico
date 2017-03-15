// <copyright file="SandboxAction.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using System;
    using System.Data.SqlClient;
    using AutoMapper;
    using Serilog;

    public class SandboxAction : IAction<SandboxArgs>
    {
        private readonly Func<SqlConnection> connectionFactory;

        public SandboxAction(Func<SqlConnection> connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public void Execute(SandboxArgs args)
        {
            using (var conn = this.connectionFactory())
            using (var session = SqlSession.Open(conn))
            {
                var repo = new SqlRepository(session);
                var cmd = new GetDataSetCommand(repo);

                var req = Mapper.Map<GetDataSetRequest>(
                    new GetDataSetArgs
                    {
                        DataSetId = args.DataSetId,
                    });

                var res = cmd.Execute(req);

                res.MatchSome(x =>
                {
                    session.Commit();
                    var fc = x.GetFeatureCollection();
                    var json = fc.ToGeoJson();
                    Console.WriteLine(json);
                });

                res.MatchNone(x =>
                {
                    session.Rollback();
                    Log.Error(
                        x,
                        "Could nog execute {ActionName} action",
                        nameof(SandboxAction));
                });
            }
        }
    }
}
