// <copyright file="NewDataSetCommand.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using Optional;

    using static Optional.Option;

    using Req = NewDataSetRequest;
    using Res = NewDataSetResponse;

    public class NewDataSetCommand : ICommand<Req, Res, Exception>
    {
        private readonly IRepository repository;

        public NewDataSetCommand(IRepository repository)
        {
            this.repository = repository;
        }

        public Option<Res, Exception> Execute(Req req)
        {
            try
            {
                var rec = this.InsertDataSet(
                    req.PlotId,
                    req.FeatureTypeId,
                    req.Name,
                    req.DateCreated);

                var res = new Res
                {
                    DataSet = rec,
                };

                return Some<Res, Exception>(res);
            }
            catch (Exception ex)
            {
                return None<Res, Exception>(ex);
            }
        }

        private DataSetRecord InsertDataSet(
            int plotId,
            int featureTypeId,
            string name,
            DateTime dateCreated)
        {
            var rec = new DataSetRecord
            {
                PlotId = plotId,
                Name = name,
                FeatureTypeId = featureTypeId,
                DateCreated = dateCreated,
            };

            rec.Id = this.repository.InsertDataSet(rec);
            return rec;
        }
    }
}
