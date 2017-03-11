// <copyright file="NewStyleAction.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>
namespace Calico.Cmd
{
    using System;
    using System.Data.SqlClient;
    using AutoMapper;
    using Serilog;

    public class NewStyleAction : IAction<NewStyleArgs>
    {
        private readonly Func<SqlConnection> connectionFactory;

        public NewStyleAction(Func<SqlConnection> connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public void Execute(NewStyleArgs args)
        {
            using (var conn = this.connectionFactory())
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    var repo = new SqlRepository(conn, tx);
                    var cmd = new NewStyleCommand(repo);
                    var req = Mapper.Map<NewStyleRequest>(args);
                    var res = cmd.Execute(req);

                    res.MatchSome(x =>
                    {
                        tx.Commit();
                        Log.Information(
                            "Created style {StyleName} with id {StyleId}",
                            x.Style.Name,
                            x.Style.Id);
                    });

                    res.MatchNone(x =>
                    {
                        tx.Rollback();
                        Log.Error(
                            x,
                            "Failed to create style {StyleName}",
                            args.Name);
                    });
                }
            }
        }
    }
}
