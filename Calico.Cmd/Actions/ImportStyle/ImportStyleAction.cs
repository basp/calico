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
            using (var session = SqlSession.Open(conn))
            {
                var repo = new SqlRepository(session);
                var features = ShapefileFeatureCollection.Create(args.PathToShapefile);
                var cmd = new ImportStyleCommand(repo, features);
                var req = Mapper.Map<ImportStyleRequest>(args);
                var res = cmd.Execute(req);

                res.MatchSome(x =>
                {
                    session.Commit();
                });

                res.MatchNone(x =>
                {
                    session.Rollback();
                });
            }
        }
    }
}
