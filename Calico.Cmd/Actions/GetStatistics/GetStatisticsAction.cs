// <copyright file="GetStatisticsAction.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using System;
    using AutoMapper;
    using Newtonsoft.Json;
    using Serilog;

    public class GetStatisticsAction : IAction<GetStatisticsArgs>
    {
        public void Execute(GetStatisticsArgs args)
        {
            var features = ShapefileFeatureCollection.Create(args.PathToShapefile);
            var cmd = new GetStatisticsCommand(features);
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
