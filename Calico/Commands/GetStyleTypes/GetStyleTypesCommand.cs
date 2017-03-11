// <copyright file="GetStyleTypesCommand.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using Optional;

    using static Optional.Option;

    using Req = GetStyleTypesRequest;
    using Res = GetStyleTypesResponse;

    public class GetStyleTypesCommand : ICommand<Req, Res, Exception>
    {
        private readonly IRepository repository;

        public GetStyleTypesCommand(IRepository repository)
        {
            this.repository = repository;
        }

        public Option<Res, Exception> Execute(Req req)
        {
            try
            {
                var styleTypes = this.repository.GetStyleTypes();
                var res = new Res { StyleTypes = styleTypes };
                return Some<Res, Exception>(res);
            }
            catch (Exception ex)
            {
                return None<Res, Exception>(ex);
            }
        }
    }
}
