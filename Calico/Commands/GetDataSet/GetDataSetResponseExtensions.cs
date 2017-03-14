// <copyright file="GetDataSetResponseExtensions.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;

    public static class GetDataSetResponseExtensions
    {
        private static readonly IDictionary<Type, Func<AttributeValueRecord, object>> ValueProviders =
            new Dictionary<Type, Func<AttributeValueRecord, object>>
            {
                [typeof(long)] = x => x.LongValue,
                [typeof(double)] = x => x.DoubleValue,
                [typeof(string)] = x => x.StringValue,
            };

        public static IFeatureCollection GetFeatureCollection(
            this GetDataSetResponse self)
        {
            return new FeatureCollection(
                self.Features,
                self.GetDataTable());
        }

        public static DataTable GetDataTable(
            this GetDataSetResponse self)
        {
            var table = new DataTable();
            var columns = self.GetDataColumns().ToArray();
            table.Columns.AddRange(columns);
            foreach (var row in self.GetDataRows(table))
            {
                table.Rows.Add(row);
            }

            return table;
        }

        public static IEnumerable<DataColumn> GetDataColumns(
            this GetDataSetResponse self)
        {
            var types = self.DataTypes.ToDictionary(
                x => x.Id,
                x => Type.GetType(x.BclType));

            return self.Attributes
                .OrderBy(x => x.Index)
                .Select(x => new DataColumn(x.Name, types[x.DataTypeId]));
        }

        private static IEnumerable<DataRow> GetDataRows(
            this GetDataSetResponse self,
            DataTable table)
        {
            var types = self.GetTypeMapping();
            var providers = self.Attributes
                .OrderBy(x => x.Index)
                .Select(x => ValueProviders[types[x.DataTypeId]])
                .ToArray();

            var features = self.AttributeValues
                .GroupBy(x => x.FeatureIndex);

            foreach (var f in features)
            {
                var values = f.OrderBy(x => x.AttributeIndex);
                var row = table.NewRow();
                foreach (var v in f)
                {
                    // TODO:
                    // Validate invariant `providers.Count() == f.Count()`
                    // In other words: make sure there's never more
                    // `AttributeValues` than `Attribute` instances for a
                    // given feature in a given data set.
                    var i = v.AttributeIndex;
                    row[i] = providers[i](v) ?? DBNull.Value;
                }

                yield return row;
            }
        }

        private static IDictionary<int, Type> GetTypeMapping(
            this GetDataSetResponse self)
        {
            return self.DataTypes.ToDictionary(
                x => x.Id,
                x => Type.GetType(x.BclType));
        }
    }
}
