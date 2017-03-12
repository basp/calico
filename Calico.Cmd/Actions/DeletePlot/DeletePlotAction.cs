// <copyright file="DeletePlotAction.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using System;
    using System.Data.SqlClient;
    using AutoMapper;
    using Serilog;

    public class DeletePlotAction : IAction<DeletePlotArgs>
    {
        private readonly Func<SqlConnection> connectionFactory;

        public DeletePlotAction(Func<SqlConnection> connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public void Execute(DeletePlotArgs args)
        {
            using (var conn = this.connectionFactory())
            {
                using (var session = SqlSession.Open(conn))
                {
                    var repo = new SqlRepository(session);
                    var cmd = new DeletePlotCommand(repo);
                    var req = Mapper.Map<DeletePlotRequest>(args);
                    var res = cmd.Execute(req);

                    res.MatchSome(x =>
                    {
                        session.Commit();
                        Log.Information(
                            "Deleted plot {PlotName} with id {PlotId}",
                            x.Plot.Name,
                            x.Plot.Id);
                    });

                    res.MatchNone(x =>
                    {
                        session.Rollback();
                        Log.Error(
                            x,
                            "Failed to delete plot {PlotId}",
                            req.Id);
                    });
                }
            }
        }
    }
}
