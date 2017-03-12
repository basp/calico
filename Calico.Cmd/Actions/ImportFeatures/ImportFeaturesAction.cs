// <copyright file="ImportFeaturesAction.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using System;
    using System.Data.SqlClient;
    using AutoMapper;
    using Serilog;

    public class ImportFeaturesAction : IAction<ImportFeaturesArgs>
    {
        private readonly Func<SqlConnection> connectionFactory;

        public ImportFeaturesAction(Func<SqlConnection> connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public void Execute(ImportFeaturesArgs args)
        {
            using (var conn = this.connectionFactory())
            using (var session = SqlSession.Open(conn))
            {
                var repo = new SqlRepository(session);
                var cmd = new ImportFeaturesCommand(repo);
                var req = Mapper.Map<ImportFeaturesRequest>(args);
                var res = cmd.Execute(req);

                res.MatchSome(x =>
                {
                    session.Commit();
                    Log.Information(
                        "Imported {FeatureCount} features from {Shapefile}",
                        x.RowCount,
                        req.PathToShapefile);
                });

                res.MatchNone(x =>
                {
                    session.Rollback();
                    Log.Error(
                        x,
                        "Could not import features from shapefile {Shapefile}",
                        req.PathToShapefile);
                });
            }
        }
    }
}
