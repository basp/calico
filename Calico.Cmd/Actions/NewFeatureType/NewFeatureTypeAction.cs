// <copyright file="NewFeatureTypeAction.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using System;
    using System.Data.SqlClient;
    using AutoMapper;
    using Serilog;

    public class NewFeatureTypeAction : IAction<NewFeatureTypeArgs>
    {
        private readonly Func<SqlConnection> connectionFactory;

        public NewFeatureTypeAction(Func<SqlConnection> connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public void Execute(NewFeatureTypeArgs args)
        {
            using (var conn = this.connectionFactory())
            using (var session = SqlSession.Open(conn))
            {
                var repo = new SqlRepository(session);
                var cmd = new NewFeatureTypeCommand(repo);
                var req = Mapper.Map<NewFeatureTypeRequest>(args);
                var res = cmd.Execute(req);

                res.MatchSome(x =>
                {
                    session.Commit();
                    Log.Information(
                        "Created new feature type {FeatureTypeName} with id {FeatureTypeId}",
                        x.FeatureType.Name,
                        x.FeatureType.Id);
                });

                res.MatchNone(x =>
                {
                    session.Rollback();
                    Log.Error(
                        x,
                        "Failed to create feature type {FeatureTypeName} for tenant {TenantId}",
                        req.Name,
                        req.TenantId);
                });
            }
        }
    }
}
