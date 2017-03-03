// <copyright file="GetDataSetsAction.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using System;
    using System.Data.SqlClient;
    using AutoMapper;
    using Newtonsoft.Json;
    using Serilog;

    public class GetDataSetsAction : IAction<GetDataSetsArgs>
    {
        private readonly Func<SqlConnection> connectionFactory;

        public GetDataSetsAction(Func<SqlConnection> connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public void Execute(GetDataSetsArgs args)
        {
            using (var conn = this.connectionFactory())
            {
                var repo = new SqlRepository(conn);
                var cmd = new GetDataSetsCommand(repo);
                var req = Mapper.Map<GetDataSetsRequest>(args);
                var res = cmd.Execute(req);

                res.MatchSome(x =>
                {
                    var json = JsonConvert.SerializeObject(x.DataSets);
                    Console.WriteLine(json);
                });

                res.MatchNone(x =>
                {
                    Log.Error(
                        x,
                        "Could not get data sets for plot {PlotId}",
                        req.PlotId);
                });
            }
        }
    }
}
