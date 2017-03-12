// <copyright file="ImportStyleCommand.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using Optional;

    using static Optional.Option;

    using Req = ImportStyleRequest;
    using Res = ImportStyleResponse;

    public class ImportStyleCommand : ICommand<Req, Res, Exception>
    {
        private readonly IRepository repository;
        private readonly IFeatureCollection features;

        public ImportStyleCommand(IRepository repository, IFeatureCollection features)
        {
            this.repository = repository;
            this.features = features;
        }

        public Option<Res, Exception> Execute(Req req)
        {
            throw new NotImplementedException();
        }
    }
}
