// <copyright file="GetCategoriesCommand.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using System.Data;
    using System.Linq;
    using Optional;

    using static Optional.Option;

    using Req = GetCategoriesRequest;
    using Res = GetCategoriesResponse;

    public class GetCategoriesCommand : ICommand<Req, Res, Exception>
    {
        private readonly IClassifier<string> classifier;
        private readonly IFeatureCollection features;

        public GetCategoriesCommand(
            IClassifier<string> classifier,
            IFeatureCollection features)
        {
            this.classifier = classifier;
            this.features = features;
        }

        public Option<Res, Exception> Execute(Req req)
        {
            try
            {
                var table = this.features.GetDataTable();
                var data = table.Rows.Cast<DataRow>()
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
