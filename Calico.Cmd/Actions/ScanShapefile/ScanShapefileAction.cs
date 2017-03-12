// <copyright file="ScanShapefileAction.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using System;
    using System.Data.SqlClient;
    using AutoMapper;
    using Newtonsoft.Json;
    using Serilog;

    public class ScanShapefileAction : IAction<ScanShapefileArgs>
    {
        private readonly Func<SqlConnection> connectionFactory;

        public ScanShapefileAction(Func<SqlConnection> connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public void Execute(ScanShapefileArgs args)
        {
            using (var conn = this.connectionFactory())
            using (var session = SqlSession.Open(conn))
            {
                var repo = new SqlRepository(session);
                var features = ShapefileFeatureCollection.Create(args.PathToShapefile);
                var cmd = new ScanShapefileCommand(repo, features);
                var req = Mapper.Map<ScanShapefileRequest>(args);
                var res = cmd.Execute(req);

                res.MatchSome(x =>
                {
                    session.Commit();
                    var json = JsonConvert.SerializeObject(x);
                    Console.WriteLine(json);
                });

                res.MatchNone(x =>
                {
                    session.Rollback();
                    Log.Error(x, "Could not scan shapefile {Shapefile}", req.PathToShapefile);
                });
            }
        }
    }
}
