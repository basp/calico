// <copyright file="ImportAttributesAction.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using System;
    using System.Data.SqlClient;
    using System.Linq;
    using AutoMapper;
    using Serilog;

    public class ImportAttributesAction : IAction<ImportAttributesArgs>
    {
        private readonly Func<SqlConnection> connectionFactory;

        public ImportAttributesAction(Func<SqlConnection> connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public void Execute(ImportAttributesArgs args)
        {
            using (var conn = this.connectionFactory())
            using (var session = SqlSession.Open(conn))
            {
                var repo = new SqlRepository(session);
                var features = ShapefileFeatureCollection.Create(args.PathToShapefile);
                var cmd = new ImportAttributesCommand(repo, features);
                var req = Mapper.Map<ImportAttributesRequest>(args);
                var res = cmd.Execute(req);

                res.MatchSome(x =>
                {
                    session.Commit();
                    Log.Information(
                        "Imported {RowCount} attributes from {Shapefile}",
                        x.Attributes.Count(),
                        req.PathToShapefile);
                });

                res.MatchNone(x =>
                {
                    session.Rollback();
                    Log.Error(
                        x,
                        "Failed to import attributes from {Shapefile}",
                        req.PathToShapefile);
                });
            }
        }
    }
}
