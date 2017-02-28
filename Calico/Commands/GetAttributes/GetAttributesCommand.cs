// <copyright file="GetAttributesCommand.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using Optional;

    using static Optional.Option;

    using Req = GetAttributesRequest;
    using Res = GetAttributesResponse;

    public class GetAttributesCommand : ICommand<Req, Res, Exception>
    {
        private IRepository repository;

        public GetAttributesCommand(IRepository repository)
        {
            this.repository = repository;
        }

        public Option<Res, Exception> Execute(Req req)
        {
            try
            {
                var attributes = this.repository.GetAttributes(req.FeatureTypeId);
                var res = new Res { Attributes = attributes };
                return Some<Res, Exception>(res);
            }
            catch (Exception ex)
            {
                return None<Res, Exception>(ex);
            }
        }
    }
}
