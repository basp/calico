// <copyright file="ImportPlotAction.cs" company="TMG">
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
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    var repo = new SqlRepository(conn, tx);
                    var cmd = new ImportPlotCommand(repo);
                    var req = Mapper.Map<ImportPlotRequest>(args);
                    var res = cmd.Execute(req);

                    res.MatchSome(x =>
                    {
                        tx.Commit();
                        Log.Information(
                            "Imported plot {PlotName} ({PlotId}) into dataset {DataSetName} ({DataSetId})",
                            x.Plot.Name,
                            x.Plot.Id,
                            x.DataSet.Name,
                            x.DataSet.Id);
                    });

                    res.MatchNone(x =>
                    {
                        tx.Rollback();
                        Log.Error(
                            x,
                            "Failed to import plot from shapefile {Shapefile}",
                            req.PathToShapefile);
                    });
                }
            }
        }
    }
}
