// <copyright file="CategorizeDataSetAction.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using System;
    using AutoMapper;
    using Calico.Classification;
    using Newtonsoft.Json;
    using Serilog;

    public class CategorizeDataSetAction : IAction<CategorizeDataSetArgs>
    {
        public void Execute(CategorizeDataSetArgs args)
        {
            var classifier = new CategorizingClassifier();
            var cmd = new CategorizeDataSetCommand(classifier);
            var req = Mapper.Map<CategorizeDataSetRequest>(args);
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
                    "Failed to categorize data set from shapefile {Shapefile",
                    args.PathToShapefile);
            });

            throw new NotImplementedException();
        }
    }
}
