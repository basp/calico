// <copyright file="GetStylesCommand.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using Optional;

    using static Optional.Option;

    using Req = GetStylesRequest;
    using Res = GetStylesResponse;

    public class GetStylesCommand : ICommand<Req, Res, Exception>
    {
        private readonly IRepository repository;

        public GetStylesCommand(IRepository repository)
        {
            this.repository = repository;
        }

        public Option<Res, Exception> Execute(Req req)
        {
            try
            {
                var styles = this.repository.GetStyles(req.FeatureTypeId);
                var res = new Res { Styles = styles };
                return Some<Res, Exception>(res);
            }
            catch (Exception ex)
            {
                return None<Res, Exception>(ex);
            }
        }
    }
}
