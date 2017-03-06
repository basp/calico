// <copyright file="QuantifyDataSetAction.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using System;
    using System.Linq;
    using AutoMapper;
    using Calico.Classification;
    using Newtonsoft.Json;
    using Serilog;

    public class QuantifyDataSetAction : IAction<QuantifyDataSetArgs>
    {
        public void Execute(QuantifyDataSetArgs args)
        {
            var classifier = new QuantileClassifier();
            var cmd = new QuantifyDataSetCommand<double>(classifier);
            var req = Mapper.Map<QuantifyDataSetRequest>(args);
            var res = cmd.Execute(req);

            res.MatchSome(x =>
            {
                var json = JsonConvert.SerializeObject(x.Result);
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
