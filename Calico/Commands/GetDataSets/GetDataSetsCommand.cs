// <copyright file="GetDataSetsCommand.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using Optional;

    using static Optional.Option;

    using Req = GetDataSetsRequest;
    using Res = GetDataSetsResponse;

    public class GetDataSetsCommand : ICommand<Req, Res, Exception>
    {
        private readonly IRepository repository;

        public GetDataSetsCommand(IRepository repository)
        {
            this.repository = repository;
        }

        public Option<Res, Exception> Execute(Req req)
        {
            try
            {
                var dataSets = this.repository.GetDataSets(req.PlotId, req.Top);
                var res = new Res { DataSets = dataSets };
                return Some<Res, Exception>(res);
            }
            catch (Exception ex)
            {
                return None<Res, Exception>(ex);
            }
        }
    }
}
