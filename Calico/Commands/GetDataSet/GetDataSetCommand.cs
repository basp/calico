// <copyright file="GetDataSetCommand.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using Optional;

    using static Optional.Option;

    using Req = GetDataSetRequest;
    using Res = GetDataSetResponse;

    public class GetDataSetCommand : ICommand<Req, Res, Exception>
    {
        private readonly IRepository repository;

        public GetDataSetCommand(IRepository repository)
        {
            this.repository = repository;
        }

        public Option<Res, Exception> Execute(Req req)
        {
            try
            {
                return None<Res, Exception>(new NotImplementedException());
            }
            catch (Exception ex)
            {
                return None<Res, Exception>(ex);
            }
        }
    }
}
