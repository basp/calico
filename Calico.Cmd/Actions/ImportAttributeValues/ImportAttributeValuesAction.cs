// <copyright file="ImportAttributeValuesAction.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using System;
    using System.Data.SqlClient;
    using AutoMapper;
    using Serilog;

    public class ImportAttributeValuesAction : IAction<ImportAttributeValuesArgs>
    {
        private readonly Func<SqlConnection> connectionFactory;

        public ImportAttributeValuesAction(Func<SqlConnection> connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public void Execute(ImportAttributeValuesArgs args)
        {
            using (var conn = this.connectionFactory())
            using (var session = SqlSession.Open(conn))
            {
                var repo = new SqlRepository(session);
                var provider = new ShapefileFeatureCollectionProvider(args.PathToShapefile);
                var cmd = new ImportAttributeValuesCommand(repo, provider.Get);
                var req = Mapper.Map<ImportAttributeValuesRequest>(args);
                var res = cmd.Execute(req);

                res.MatchSome(x =>
                {
                    session.Commit();
                    Log.Information(
                        "Imported {RowCount} values from {Shapefile} into data set {DataSetId}",
                        x.RowCount,
                        args.PathToShapefile,
                        args.DataSetId);
                });

                res.MatchNone(x =>
                {
                    session.Rollback();
                    Log.Error(
                        x,
                        "Failed to import attribute values from {Shapefile}",
                        args.PathToShapefile);
                });
            }
        }
    }
}
