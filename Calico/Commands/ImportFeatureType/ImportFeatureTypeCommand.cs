// <copyright file="ImportFeatureTypeCommand.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using System.Collections.Generic;
    using Optional;
    using Optional.Linq;

    using Req = ImportFeatureTypeRequest;
    using Res = ImportFeatureTypeResponse;

    public class ImportFeatureTypeCommand : ICommand<Req, Res, Exception>
    {
        private readonly IRepository repository;
        private readonly Func<Option<IFeatureCollection, Exception>> featureCollectionProvider;

        public ImportFeatureTypeCommand(
            IRepository repository,
            Func<Option<IFeatureCollection, Exception>> featureCollectionProvider)
        {
            this.repository = repository;
            this.featureCollectionProvider = featureCollectionProvider;
        }

        public Option<Res, Exception> Execute(Req req)
        {
            return from featureType in this.InsertFeatureType(req)
                   from attrs in this.ImportAttributes(featureType.Id, req)
                   let tenant = this.repository.GetTenant(req.TenantId)
                   select new Res
                   {
                       Tenant = tenant,
                       FeatureType = featureType,
                       Attributes = attrs,
                   };
        }

        public Option<FeatureTypeRecord, Exception> InsertFeatureType(Req req)
        {
            var cmd = new NewFeatureTypeCommand(this.repository);
            var res = cmd.Execute(new NewFeatureTypeRequest
            {
                TenantId = req.TenantId,
                Name = req.Name,
            });

            return res.Map(x => x.FeatureType);
        }

        private Option<IEnumerable<AttributeRecord>, Exception> ImportAttributes(
            int featureTypeId,
            Req req)
        {
            var cmd = new ImportAttributesCommand(this.repository, this.featureCollectionProvider);
            var res = cmd.Execute(new ImportAttributesRequest
            {
                FeatureTypeId = featureTypeId,
            });

            return res.Map(x => x.Attributes);
        }
    }
}
