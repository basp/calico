// <copyright file="GetCategoriesAction.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using System;
    using AutoMapper;
    using Calico.Visualization;
    using Newtonsoft.Json;
    using Serilog;

    public class GetCategoriesAction : IAction<GetCategoriesArgs>
    {
        public void Execute(GetCategoriesArgs args)
        {
            var classifier = new CategorizingClassifier();
            var provider = new ShapefileFeatureCollectionProvider(args.PathToShapefile);
            var cmd = new GetCategoriesCommand(classifier, provider.Get);
            var req = Mapper.Map<GetCategoriesRequest>(args);
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
        }
    }
}
