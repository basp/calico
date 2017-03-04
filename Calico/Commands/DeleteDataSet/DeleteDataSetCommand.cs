// <copyright file="DeleteDataSetCommand.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using Optional;
    using Serilog;

    using static Optional.Option;

    using Req = DeleteDataSetRequest;
    using Res = DeleteDataSetResponse;

    public class DeleteDataSetCommand : ICommand<Req, Res, Exception>
    {
        private IRepository repository;

        public DeleteDataSetCommand(IRepository repository)
        {
            this.repository = repository;
        }

        public Option<Res, Exception> Execute(Req req)
        {
            try
            {
                var dataSet = this.repository.GetDataSet(req.Id);
                Log.Debug(
                    "Deleting data set {DataSetName} with id {DataSetId}",
                    dataSet.Name,
                    dataSet.Id);

                var modified = this.repository.DeleteDataSet(req.Id);
                var res = new Res
                {
                    DataSet = dataSet,
                    RowCount = modified,
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
