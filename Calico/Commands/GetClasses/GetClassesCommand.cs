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
    using Optional.Linq;
    using Visualization;

    using static Optional.Option;

    using Req = GetClassesRequest;
    using Res = GetClassesResponse;

    public class GetClassesCommand<T>
        : ICommand<Req, Res, Exception>
    {
        private readonly IClassifier<double> classifier;
        private readonly Chauvenet chauvenet = new Chauvenet(Normal.Standard);
        private readonly Func<Option<IFeatureCollection, Exception>> featureCollectionProvider;

        public GetClassesCommand(
            IClassifier<double> classifier,
            Func<Option<IFeatureCollection, Exception>> featureCollectionProvider)
        {
            this.classifier = classifier;
            this.featureCollectionProvider = featureCollectionProvider;
        }

        public Option<Res, Exception> Execute(Req req)
        {
            try
            {
                IEnumerable<double> outliers = new double[0];
                return from features in this.featureCollectionProvider()
                       let table = features.DataTable
                       let data = table.Rows.Cast<DataRow>().Select(x => Convert.ToDouble(x[req.ColumnName]))
                       let normalized = this.RemoveOutliers(data, out outliers)
                       let buckets = this.classifier.Classify(req.Normalize ? normalized : data)
                       select new Res
                       {
                           Classes = buckets.ToList(),
                           Outliers = outliers.ToList(),
                       };
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
