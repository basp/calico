﻿// <copyright file="ImportPlotAction.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using System;
    using System.Data.SqlClient;
    using System.IO;
    using AutoMapper;
    using Serilog;

    public class ImportPlotAction : IAction<ImportPlotArgs>
    {
        private readonly Func<SqlConnection> connectionFactory;

        public ImportPlotAction(Func<SqlConnection> connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public void Execute(ImportPlotArgs args)
        {
            args.Name = string.IsNullOrWhiteSpace(args.Name)
                ? Path.GetFileNameWithoutExtension(args.PathToShapefile)
                : args.Name;

            using (var conn = this.connectionFactory())
            using (var session = SqlSession.Open(conn))
            {
                var repo = new SqlRepository(session);
                var provider = new ShapefileFeatureCollectionProvider(args.PathToShapefile);
                var cmd = new ImportPlotCommand(repo, provider.Get);
                var req = Mapper.Map<ImportPlotRequest>(args);
                var res = cmd.Execute(req);

                res.MatchSome(x =>
                {
                    session.Commit();
                    Log.Information(
                        "Imported plot {PlotName} ({PlotId}) into dataset {DataSetName} ({DataSetId})",
                        x.Plot.Name,
                        x.Plot.Id,
                        x.DataSet.Name,
                        x.DataSet.Id);
                });

                res.MatchNone(x =>
                {
                    session.Rollback();
                    Log.Error(
                        x,
                        "Failed to import plot from shapefile {Shapefile}",
                        args.PathToShapefile);
                });
            }
        }
    }
}
