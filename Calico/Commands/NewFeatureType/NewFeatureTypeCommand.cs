﻿// <copyright file="NewFeatureTypeCommand.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using Optional;

    using static Optional.Option;

    using Req = NewFeatureTypeRequest;
    using Res = NewFeatureTypeResponse;

    public class NewFeatureTypeCommand : ICommand<Req, Res, Exception>
    {
        private readonly IRepository repository;

        public NewFeatureTypeCommand(IRepository repository)
        {
            this.repository = repository;
        }

        public Option<Res, Exception> Execute(Req req)
        {
            try
            {
                var rec = this.InsertFeatureType(req.TenantId, req.Name);
                var res = new Res { FeatureType = rec };
                return Some<Res, Exception>(res);
            }
            catch (Exception ex)
            {
                return None<Res, Exception>(ex);
            }
        }

        private FeatureTypeRecord InsertFeatureType(int tenantId, string name)
        {
            var rec = new FeatureTypeRecord
            {
                TenantId = tenantId,
                Name = name,
            };

            rec.Id = this.repository.InsertFeatureType(rec);
            return rec;
        }
    }
}
