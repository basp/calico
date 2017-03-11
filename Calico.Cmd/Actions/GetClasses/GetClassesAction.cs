// <copyright file="GetClassesAction.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using System;
    using AutoMapper;
    using Calico.Visualization;
    using Newtonsoft.Json;
    using Serilog;

    public class GetClassesAction : IAction<GetClassesArgs>
    {
        public void Execute(GetClassesArgs args)
        {
            var classifier = new NestedMeansClassifier(args.Depth);
            var cmd = new GetClassesCommand<double>(classifier);
            var req = Mapper.Map<GetClassesRequest>(args);
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
                    "Failed to quantify data set from shapefile {Shapefile}",
                    args.PathToShapefile);
            });
        }
    }
}
