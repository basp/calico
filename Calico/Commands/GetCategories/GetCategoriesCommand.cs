// <copyright file="GetCategoriesCommand.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using System.Data;
    using System.Linq;
    using DotSpatial.Data;
    using Optional;

    using static Optional.Option;

    using Req = GetCategoriesRequest;
    using Res = GetCategoriesResponse;

    public class GetCategoriesCommand : ICommand<Req, Res, Exception>
    {
        private readonly IClassifier<string> classifier;

        public GetCategoriesCommand(IClassifier<string> classifier)
        {
            this.classifier = classifier;
        }

        public Option<Res, Exception> Execute(Req req)
        {
            try
            {
                var shapefile = Shapefile.OpenFile(req.PathToShapefile);
                var data = shapefile.DataTable.Rows
                    .Cast<DataRow>()
                    .Select(x => x[req.ColumnName])
                    .Select(x => x.ToString());

                var buckets = this.classifier.Classify(data);
                var res = new Res
                {
                    Result = buckets,
                };

                return Some<Res, Exception>(res);
            }
            catch (Exception ex)
            {
                return None<Res, Exception>(ex);
            }
        }
    }
}
