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
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    var repo = new SqlRepository(conn, tx);
                    var cmd = new ImportFeatureTypeCommand(repo);
                    var req = Mapper.Map<ImportFeatureTypeRequest>(args);
                    var res = cmd.Execute(req);

                    res.MatchSome(x =>
                    {
                        tx.Commit();
                        Log.Information(
                            "Imported feature type {FeatureTypeName} ({FeatureTypeId}) for client {ClientName} ({ClientId})",
                            x.FeatureType.Name,
                            x.FeatureType.Id,
                            x.Client.Name,
                            x.Client.Id);
                    });

                    res.MatchNone(x =>
                    {
                        tx.Rollback();
                        Log.Error(
                            x,
                            "Failed to import feature type from shapefile {Shapefile}",
                            req.PathToShapefile);
                    });
                }
            }
        }
    }
}
