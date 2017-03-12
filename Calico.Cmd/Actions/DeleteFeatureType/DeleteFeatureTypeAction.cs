// <copyright file="DeleteFeatureTypeAction.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using System;
    using System.Data.SqlClient;
    using AutoMapper;
    using Serilog;

    public class DeleteFeatureTypeAction : IAction<DeleteFeatureTypeArgs>
    {
        private readonly Func<SqlConnection> connectionFactory;

        public DeleteFeatureTypeAction(Func<SqlConnection> connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public void Execute(DeleteFeatureTypeArgs args)
        {
            using (var conn = this.connectionFactory())
            {
                using (var session = SqlSession.Open(conn))
                {
                    var repo = new SqlRepository(session);
                    var cmd = new DeleteFeatureTypeCommand(repo);
                    var req = Mapper.Map<DeleteFeatureTypeRequest>(args);
                    var res = cmd.Execute(req);

                    res.MatchSome(x =>
                    {
                        session.Commit();
                        Log.Information(
                            "Deleted feature type {FeatureTypeName} with id {FeatureTypeId}",
                            x.FeatureType.Name,
                            x.FeatureType.Id);
                    });

                    res.MatchNone(x =>
                    {
                        session.Rollback();
                        Log.Error(
                            x,
                            "Failed to delete feature type {FeatureTypeId}",
                            req.Id);
                    });
                }
            }
        }
    }
}
