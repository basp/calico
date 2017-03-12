// <copyright file="GetCategoriesCommand.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Optional;
    using Optional.Linq;

    using static Optional.Option;

    using Req = GetCategoriesRequest;
    using Res = GetCategoriesResponse;

    public class GetCategoriesCommand : ICommand<Req, Res, Exception>
    {
        private readonly IClassifier<string> classifier;
        private readonly Func<Option<IFeatureCollection, Exception>> featureCollectionProvider;

        public GetCategoriesCommand(
            IClassifier<string> classifier,
            Func<Option<IFeatureCollection, Exception>> featureCollectionProvider)
        {
            this.classifier = classifier;
            this.featureCollectionProvider = featureCollectionProvider;
        }

        public Option<Res, Exception> Execute(Req req)
        {
            try
            {
                return from features in this.featureCollectionProvider()
                       let data = GetData(features.DataTable, req.ColumnName)
                       let buckets = this.classifier.Classify(data)
                       select new Res { Result = buckets };
            }
            catch (Exception ex)
            {
                return None<Res, Exception>(ex);
            }
        }

        private static IEnumerable<string> GetData(DataTable table, string columnName)
        {
            return table.Rows.Cast<DataRow>().Select(x => x[columnName].ToString());
        }
    }
}
