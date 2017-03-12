// <copyright file="GetClassesCommand.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using MathNet.Numerics.Statistics;
    using Optional;
    using Visualization;

    using static Optional.Option;

    using Req = GetClassesRequest;
    using Res = GetClassesResponse;

    public class GetClassesCommand<T>
        : ICommand<Req, Res, Exception>
    {
        private readonly IClassifier<double> classifier;
        private readonly Chauvenet chauvenet = new Chauvenet(Normal.Standard);
        private readonly IFeatureCollection features;

        public GetClassesCommand(
            IClassifier<double> classifier,
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
                    .Select(x => Convert.ToDouble(x));

                var normalized = this.RemoveOutliers(data, out IEnumerable<double> outliers);
                var buckets = this.classifier.Classify(req.Normalize ? normalized : data);
                var res = new Res
                {
                    Classes = buckets.ToList(),
                    Outliers = outliers.ToList(),
                };

                return Some<Res, Exception>(res);
            }
            catch (Exception ex)
            {
                return None<Res, Exception>(ex);
            }
        }

        private IEnumerable<double> RemoveOutliers(IEnumerable<double> data, out IEnumerable<double> outliers)
        {
            var sampleSize = data.Count();
            var mean = Statistics.Mean(data);
            var standardDeviation = Statistics.StandardDeviation(data);
            var f = this.chauvenet.Create(sampleSize, mean, standardDeviation);

            const double threshold = 0.5;
            outliers = data.Where(x => f(x) < threshold);
            return data.Where(x => f(x) > threshold);
        }
    }
}
