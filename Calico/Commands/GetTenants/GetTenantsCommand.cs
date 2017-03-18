// <copyright file="GetTenantsCommand.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using Optional;

    using static Optional.Option;

    using Req = GetTenantsRequest;
    using Res = GetTenantsResponse;

    public class GetTenantsCommand : ICommand<Req, Res, Exception>
    {
        private readonly IRepository repository;

        public GetTenantsCommand(IRepository repository)
        {
            this.repository = repository;
        }

        public Option<Res, Exception> Execute(Req req)
        {
            try
            {
                var tenants = this.repository.GetTenants(first: req.Top, after: 0);
                var res = new Res { Tenants = tenants };
                return Some<Res, Exception>(res);
            }
            catch (Exception ex)
            {
                return None<Res, Exception>(ex);
            }
        }
    }
}
