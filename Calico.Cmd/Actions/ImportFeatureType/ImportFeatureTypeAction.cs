// <copyright file="ImportFeatureTypeAction.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using System;
    using System.Data.SqlClient;
    using AutoMapper;
    using Serilog;

    public class ImportFeatureTypeAction : IAction<ImportFeatureTypeArgs>
    {
        private readonly Func<SqlConnection> connectionFactory;

        public ImportFeatureTypeAction(Func<SqlConnection> connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public void Execute(ImportFeatureTypeArgs args)
        {
            using (var conn = this.connectionFactory())
            using (var session = SqlSession.Open(conn))
            {
                var repo = new SqlRepository(session);
                var features = ShapefileFeatureCollection.Create(args.PathToShapefile);
                var cmd = new ImportFeatureTypeCommand(repo, features);
                var req = Mapper.Map<ImportFeatureTypeRequest>(args);
                var res = cmd.Execute(req);

                res.MatchSome(x =>
                {
                    session.Commit();
                    Log.Information(
                        "Imported feature type {FeatureTypeName} ({FeatureTypeId}) for client {ClientName} ({ClientId})",
                        x.FeatureType.Name,
                        x.FeatureType.Id,
                        x.Client.Name,
                        x.Client.Id);
                });

                res.MatchNone(x =>
                {
                    session.Rollback();
                    Log.Error(
                        x,
                        "Failed to import feature type from shapefile {Shapefile}",
                        args.PathToShapefile);
                });
            }
        }
    }
}