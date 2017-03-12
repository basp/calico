// <copyright file="NewClientAction.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using System;
    using System.Data.SqlClient;
    using AutoMapper;
    using Serilog;

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
            using (var session = SqlSession.Open(conn))
            {
                var repo = new SqlRepository(session);
                var cmd = new NewClientCommand(repo);
                var req = Mapper.Map<NewClientRequest>(args);
                var res = cmd.Execute(req);

                res.MatchSome(x =>
                {
                    session.Commit();
                    Log.Information(
                        "Created client {ClientName} with id {ClientId}",
                        x.Client.Name,
                        x.Client.Id);
                });

                res.MatchNone(x =>
                {
                    session.Rollback();
                    Log.Error(x, "Failed to create client");
                });
            }
        }
    }
}
