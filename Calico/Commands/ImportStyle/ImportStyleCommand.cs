// <copyright file="ImportStyleCommand.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using Optional;

    using Req = ImportStyleRequest;
    using Res = ImportStyleResponse;

    public class ImportStyleCommand : ICommand<Req, Res, Exception>
    {
        private readonly IRepository repository;
        private readonly Func<Option<IFeatureCollection, Exception>> featureCollectionProvider;

        public ImportStyleCommand(
            IRepository repository,
            Func<Option<IFeatureCollection, Exception>> featureCollectionProvider)
        {
            this.repository = repository;
            this.featureCollectionProvider = featureCollectionProvider;
        }

        public Option<Res, Exception> Execute(Req req)
        {
            throw new NotImplementedException();
        }
    }
}
