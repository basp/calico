// <copyright file="ImportStyleAction.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using System;
    using System.Data.SqlClient;
    using AutoMapper;

    public class ImportStyleAction : IAction<ImportStyleArgs>
    {
        private readonly Func<SqlConnection> connectionFactory;

        public ImportStyleAction(Func<SqlConnection> connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public void Execute(ImportStyleArgs args)
        {
            using (var conn = this.connectionFactory())
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    var repo = new SqlRepository(conn, tx);
                    var cmd = new ImportStyleCommand(repo);
                    var req = Mapper.Map<ImportStyleRequest>(args);
                    var res = cmd.Execute(req);

                    res.MatchSome(x =>
                    {
                        tx.Commit();
                    });

                    res.MatchNone(x =>
                    {
                        tx.Rollback();
                    });
                }
            }
        }
    }
}
