// <copyright file="NewPlotAction.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using System;
    using System.Data.SqlClient;
    using AutoMapper;
    using Serilog;

    public class NewPlotAction : IAction<NewPlotArgs>
    {
        private readonly Func<SqlConnection> connectionFactory;

        public NewPlotAction(
            Func<SqlConnection> connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public void Execute(NewPlotArgs args)
        {
            using (var conn = this.connectionFactory())
            using (var session = SqlSession.Open(conn))
            {
                var repo = new SqlRepository(session);
                var provider = new ShapefileFeatureCollectionProvider(args.PathToShapefile);
                var cmd = new NewPlotCommand(repo, provider.Get);
                var req = Mapper.Map<NewPlotRequest>(args);
                var res = cmd.Execute(req);

                res.MatchSome(x =>
                {
                    session.Commit();
                    Log.Information(
                        "Created plot {PlotName} with id {PlotId}",
                        x.Plot.Name,
                        x.Plot.Id);
                });

                res.MatchNone(x =>
                {
                    session.Rollback();
                    Log.Error(
                        x,
                        "Failed to create plot from shapefile {Shapefile}",
                        args.PathToShapefile);
                });
            }
        }
    }
}
