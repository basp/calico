﻿// <copyright file="ImportFeatureTypeCommand.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using System.Collections.Generic;
    using Optional;
    using Optional.Linq;

    using static Optional.Option;

    using Req = ImportFeatureTypeRequest;
    using Res = ImportFeatureTypeResponse;

    public class ImportFeatureTypeCommand : ICommand<Req, Res, Exception>
    {
        private readonly IRepository repository;

        public ImportFeatureTypeCommand(IRepository repository)
        {
            this.repository = repository;
        }

        public Option<Res, Exception> Execute(Req req)
        {
            return from featureType in this.InsertFeatureType(req)
                   from attrs in this.ImportAttributes(featureType.Id, req)
                   let client = this.repository.GetClient(req.ClientId)
                   select new Res
                   {
                       Client = client,
                       FeatureType = featureType,
                       Attributes = attrs,
                   };
        }

        public Option<FeatureTypeRecord, Exception> InsertFeatureType(Req req)
        {
            var cmd = new NewFeatureTypeCommand(this.repository);
            var res = cmd.Execute(new NewFeatureTypeRequest
            {
                ClientId = req.ClientId,
                Name = req.Name,
            });

            return res.Map(x => x.FeatureType);
        }

        private Option<IEnumerable<AttributeRecord>, Exception> ImportAttributes(
            int featureTypeId,
            Req req)
        {
            var cmd = new ImportAttributesCommand(this.repository);
            var res = cmd.Execute(new ImportAttributesRequest
            {
                FeatureTypeId = featureTypeId,
                PathToShapefile = req.PathToShapefile,
            });

            return res.Map(x => x.Attributes);
        }
    }
}