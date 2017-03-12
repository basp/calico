// <copyright file="ImportDataSetAction.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using System;
    using System.Data.SqlClient;
    using System.IO;
    using AutoMapper;
    using Serilog;

    public class ImportDataSetAction : IAction<ImportDataSetArgs>
    {
        private readonly Func<SqlConnection> connectionFactory;

        public ImportDataSetAction(Func<SqlConnection> connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public void Execute(ImportDataSetArgs args)
        {
            args.Name = string.IsNullOrWhiteSpace(args.Name)
                ? Path.GetFileNameWithoutExtension(args.PathToShapefile)
                : args.Name;

            using (var conn = this.connectionFactory())
            using (var session = SqlSession.Open(conn))
            {
                var repo = new SqlRepository(session);
                var features = ShapefileFeatureCollection.Create(args.PathToShapefile);
                var cmd = new ImportDataSetCommand(repo, features);
                var req = Mapper.Map<ImportDataSetRequest>(args);
                var res = cmd.Execute(req);

                res.MatchSome(x =>
                {
                    session.Commit();
                    Log.Information(
                        "Imported {DataSetName} ({DataSetId}) for plot {PlotName} ({PlotId})",
                        x.DataSet.Name,
                        x.DataSet.Id,
                        x.Plot.Name,
                        x.Plot.Id);
                });

                res.MatchNone(x =>
                {
                    session.Rollback();
                    Log.Error(
                        x,
                        "Failed to import data set from shapefile {Shapefile}",
                        args.PathToShapefile);
                });
            }
        }
    }
}
