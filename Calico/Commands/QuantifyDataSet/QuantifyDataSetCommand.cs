// <copyright file="QuantifyDataSetCommand.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using System.Data;
    using System.Linq;
    using Classification;
    using DotSpatial.Data;
    using Optional;

    using static Optional.Option;

    using Req = QuantifyDataSetRequest;
    using Res = QuantifyDataSetResponse<double>;

    public class QuantifyDataSetCommand<T>
        : ICommand<Req, Res, Exception>
    {
        private readonly IQuantifyingClassifier classifier;

        public QuantifyDataSetCommand(IQuantifyingClassifier classifier)
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
                    .Select(x => Convert.ToDouble(x));

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
