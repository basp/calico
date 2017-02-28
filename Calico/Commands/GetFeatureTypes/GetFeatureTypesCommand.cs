// <copyright file="GetFeatureTypesCommand.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using Optional;

    using static Optional.Option;

    using Req = GetFeatureTypesRequest;
    using Res = GetFeatureTypesResponse;

    public class GetFeatureTypesCommand : ICommand<Req, Res, Exception>
    {
        private readonly IRepository repository;

        public GetFeatureTypesCommand(IRepository repository)
        {
            this.repository = repository;
        }

        public Option<Res, Exception> Execute(Req req)
        {
            try
            {
                var featureTypes = this.repository.GetFeatureTypes(req.ClientId, req.Top);
                var res = new Res { FeatureTypes = featureTypes };
                return Some<Res, Exception>(res);
            }
            catch (Exception ex)
            {
                return None<Res, Exception>(ex);
            }
        }
    }
}
