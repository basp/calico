// <copyright file="ScanShapefileCommand.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using DotSpatial.Data;
    using Optional;

    using static Optional.Option;

    using Req = ScanShapefileRequest;
    using Res = ScanShapefileResponse;

    public class ScanShapefileCommand : ICommand<Req, Res, Exception>
    {
        private readonly IRepository repository;

        public ScanShapefileCommand(IRepository repository)
        {
            this.repository = repository;
        }

        public Option<Res, Exception> Execute(Req req)
        {
            try
            {
                var shapefile = Shapefile.OpenFile(req.PathToShapefile);
                var featureTypes = this.GetMatchingFeatureTypes(
                    req.ClientId,
                    shapefile.DataTable);

                var numberOfFeatures = shapefile.Features.Count;

                var t = shapefile.DataTable;
                var cols = t.Columns.Cast<DataColumn>();
                var attributes = cols
                    .Select(x => GetStatisticsCommand.GetStatistics(t, x))
                    .ToList();

                var feature = shapefile.Features[0].BasicGeometry.ToString();
                var res = new Res
                {
                    PathToShapefile = req.PathToShapefile,
                    NumberOfFeatures = numberOfFeatures,
                    Attributes = attributes,
                    FeatureTypes = featureTypes,
                    Plots = this.GetMatchingPlots(req.ClientId, feature, req.SRID),
                };

                return Some<Res, Exception>(res);
            }
            catch (Exception ex)
            {
                return None<Res, Exception>(ex);
            }
        }

        private string ComputeHash(MD5 md5, FeatureTypeRecord featureType)
        {
            var attrs = this.repository.GetAttributes(featureType.Id);
            var chunks = attrs
                .Select(x => string.Concat(x.Name, x.DataTypeId))
                .ToArray();

            var str = string.Join(string.Empty, chunks);
            var buf = Encoding.UTF8.GetBytes(str);
            var hash = md5.ComputeHash(buf);
            return Convert.ToBase64String(hash);
        }

        private string ComputeHash(MD5 md5, DataColumn[] columns)
        {
            var dataTypes = this.repository.GetDataTypes()
                .ToDictionary(x => x.BclType, x => x);

            var chunks = columns
                .Select(x => string.Concat(x.ColumnName, dataTypes[x.DataType.FullName].Id))
                .ToArray();

            var str = string.Join(string.Empty, chunks);
            var buf = Encoding.UTF8.GetBytes(str);
            var hash = md5.ComputeHash(buf);
            return Convert.ToBase64String(hash);
        }

        private IEnumerable<PlotRecord> GetMatchingPlots(
            int clientId,
            string geometry,
            int srid)
        {
            return this.repository.GetPlotsContainingGeometry(
                clientId,
                geometry,
                srid);
        }

        private IEnumerable<FeatureTypeRecord> GetMatchingFeatureTypes(
            int clientId,
            DataTable table)
        {
            var md5 = MD5.Create();

            var stack = this.repository
                .GetFeatureTypes(clientId, 100)
                .ToDictionary(x => this.ComputeHash(md5, x), x => x);

            var columns = table.Columns.Cast<DataColumn>().ToArray();
            var needle = this.ComputeHash(md5, columns);
            foreach (var x in stack)
            {
                if (x.Key == needle)
                {
                    yield return x.Value;
                }
            }
        }
    }
}
