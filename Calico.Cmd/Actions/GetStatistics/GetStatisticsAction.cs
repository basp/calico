// <copyright file="GetStatisticsAction.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using System;
    using System.Data.SqlClient;
    using AutoMapper;
    using Newtonsoft.Json;
    using Serilog;

    public class GetStatisticsAction : IAction<GetStatisticsArgs>
    {
        public void Execute(GetStatisticsArgs args)
        {
            var cmd = new GetStatisticsCommand();
            var req = Mapper.Map<GetStatisticsRequest>(args);
            var res = cmd.Execute(req);

            res.MatchSome(x =>
            {
                var json = JsonConvert.SerializeObject(x);
                Console.WriteLine(json);
            });

            res.MatchNone(x =>
            {
                Log.Error(
                    x,
                    "Could not get statistics for shapefile {Shapefile}",
                    args.PathToShapefile);
            });
        }
    }
}
