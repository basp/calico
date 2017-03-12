// <copyright file="GetStatisticsCommand.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using System.Data;
    using System.Linq;
    using MathNet.Numerics.Statistics;
    using Optional;
    using Optional.Linq;

    using Req = GetStatisticsRequest;
    using Res = GetStatisticsResponse;

    public class GetStatisticsCommand : ICommand<Req, Res, Exception>
    {
        private readonly Func<Option<IFeatureCollection, Exception>> featureCollectionProvider;

        public GetStatisticsCommand(Func<Option<IFeatureCollection, Exception>> featureCollectionProvider)
        {
            this.featureCollectionProvider = featureCollectionProvider;
        }

        public static AttributeStatistics GetStatistics(DataTable table, DataColumn column)
        {
            var data = table.Rows
                .Cast<DataRow>()
                .Select(x => x[column]);

            var stats = column.DataType == typeof(double)
                ? new DescriptiveStatistics(data.Cast<double>())
                : new DescriptiveStatistics(Enumerable.Empty<double>());

            return new AttributeStatistics
            {
                Attribute = AttributeProxy.Create(column),
                Mean = stats.Mean,
                Variance = stats.Variance,
                StandardDeviation = stats.StandardDeviation,
                Skewness = stats.Skewness,
                Kurtosis = stats.Kurtosis,
                Maximum = stats.Maximum,
                Minimum = stats.Minimum,
            };
        }

        public Option<Res, Exception> Execute(Req req)
        {
            return from features in this.featureCollectionProvider()
                   let table = features.DataTable
                   let cols = table.Columns.Cast<DataColumn>()
                   let stats = cols.Select(x => GetStatistics(table, x))
                   select new Res { Result = stats.ToList() };
        }
    }
}
