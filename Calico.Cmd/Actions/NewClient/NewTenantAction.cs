// <copyright file="NewTenantAction.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using System;
    using System.Data.SqlClient;
    using AutoMapper;
    using Serilog;

    public class NewTenantAction : IAction<NewTenantArgs>
    {
        private readonly Func<SqlConnection> connectionFactory;

        public NewTenantAction(Func<SqlConnection> connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public void Execute(NewTenantArgs args)
        {
            using (var conn = this.connectionFactory())
            using (var session = SqlSession.Open(conn))
            {
                var repo = new SqlRepository(session);
                var cmd = new NewTenantCommand(repo);
                var req = Mapper.Map<NewTenantRequest>(args);
                var res = cmd.Execute(req);

                res.MatchSome(x =>
                {
                    session.Commit();
                    Log.Information(
                        "Created tenant {TenantName} with id {TenantId}",
                        x.Tenant.Name,
                        x.Tenant.Id);
                });

                res.MatchNone(x =>
                {
                    session.Rollback();
                    Log.Error(x, "Failed to create tenant");
                });
            }
        }
    }
}
