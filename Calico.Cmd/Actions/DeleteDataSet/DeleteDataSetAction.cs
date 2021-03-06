﻿// <copyright file="DeleteDataSetAction.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using System;
    using System.Data.SqlClient;
    using AutoMapper;
    using Serilog;

    public class DeleteDataSetAction : IAction<DeleteDataSetArgs>
    {
        private readonly Func<SqlConnection> connectionFactory;

        public DeleteDataSetAction(Func<SqlConnection> connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public void Execute(DeleteDataSetArgs args)
        {
            using (var conn = this.connectionFactory())
            {
                using (var session = SqlSession.Open(conn))
                {
                    var repo = new SqlRepository(session);
                    var cmd = new DeleteDataSetCommand(repo);
                    var req = Mapper.Map<DeleteDataSetRequest>(args);
                    var res = cmd.Execute(req);

                    res.MatchSome(x =>
                    {
                        session.Commit();
                        Log.Information(
                            "Deleted data set {DataSetName} with id {DataSetId}",
                            x.DataSet.Name,
                            x.DataSet.Id);
                    });

                    res.MatchNone(x =>
                    {
                        session.Rollback();
                        Log.Error(
                            x,
                            "Failed to delete data set {DataSetId}",
                            req.Id);
                    });
                }
            }
        }
    }
}
