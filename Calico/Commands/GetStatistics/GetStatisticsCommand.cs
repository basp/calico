// <copyright file="GetStatisticsCommand.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using DotSpatial.Data;
    using MathNet.Numerics.Statistics;
    using Optional;

    using static Optional.Option;

    using Req = GetStatisticsRequest;
    using Res = GetStatisticsResponse;

    public class GetStatisticsCommand : ICommand<Req, Res, Exception>
    {
        public GetStatisticsCommand()
        {
        }

        public Option<Res, Exception> Execute(Req req)
        {
            try
            {
                var shapefile = Shapefile.OpenFile(req.PathToShapefile);
                var t = shapefile.DataTable;
                var stats = shapefile.GetColumns()
                    .Select(x => GetStatistics(t, x))
                    .ToList();

                var res = new Res { Result = stats };
                return Some<Res, Exception>(res);
            }
            catch (Exception ex)
            {
                return None<Res, Exception>(ex);
            }
        }

        private static AttributeStatistics GetStatistics(DataTable table, DataColumn column)
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
    }
}
