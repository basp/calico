// <copyright file="GetPlotsCommand.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using Optional;

    using static Optional.Option;

    using Req = GetPlotsRequest;
    using Res = GetPlotsResponse;

    public class GetPlotsCommand : ICommand<Req, Res, Exception>
    {
        private readonly IRepository repository;

        public GetPlotsCommand(IRepository repository)
        {
            this.repository = repository;
        }

        public Option<Res, Exception> Execute(Req req)
        {
            try
            {
                var plots = this.repository.GetPlots(req.TenantId, req.Top);
                var res = new Res { Plots = plots };
                return Some<Res, Exception>(res);
            }
            catch (Exception ex)
            {
                return None<Res, Exception>(ex);
            }
        }
    }
}
