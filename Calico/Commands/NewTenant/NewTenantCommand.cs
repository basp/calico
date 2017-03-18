// <copyright file="NewTenantCommand.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using Optional;

    using static Optional.Option;

    using Req = NewTenantRequest;
    using Res = NewTenantResponse;

    public class NewTenantCommand : ICommand<Req, Res, Exception>
    {
        private readonly IRepository repository;

        public NewTenantCommand(IRepository repository)
        {
            this.repository = repository;
        }

        public Option<Res, Exception> Execute(Req req)
        {
            try
            {
                var rec = this.InsertTenant(req.Name);
                var res = new Res { Tenant = rec };
                return Some<Res, Exception>(res);
            }
            catch (Exception ex)
            {
                return None<Res, Exception>(ex);
            }
        }

        private TenantRecord InsertTenant(string name)
        {
            var rec = new TenantRecord
            {
                Name = name,
            };

            rec.Id = this.repository.InsertTenant(rec);
            return rec;
        }
    }
}
