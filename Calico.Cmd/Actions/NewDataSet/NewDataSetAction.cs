// <copyright file="NewDataSetAction.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using System;
    using System.Data.SqlClient;
    using AutoMapper;
    using Serilog;

    public class NewDataSetAction : IAction<NewDataSetArgs>
    {
        private readonly Func<SqlConnection> connectionFactory;

        public NewDataSetAction(Func<SqlConnection> connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public void Execute(NewDataSetArgs args)
        {
            using (var conn = this.connectionFactory())
            using (var session = SqlSession.Open(conn))
            {
                var repo = new SqlRepository(session);
                var cmd = new NewDataSetCommand(repo);
                var req = Mapper.Map<NewDataSetRequest>(args);
                var res = cmd.Execute(req);

                res.MatchSome(x =>
                {
                    session.Commit();
                    Log.Information(
                        "Created data set {DataSetName} with id {DataSetId}",
                        x.DataSet.Name,
                        x.DataSet.Id);
                });

                res.MatchNone(x =>
                {
                    session.Rollback();
                    Log.Error(
                        x,
                        "Failed to create data set {DataSetName} for plot {PlotId}",
                        req.Name,
                        req.PlotId);
                });
            }
        }
    }
}
