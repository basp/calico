﻿namespace Calico
{
    using System;
    using Optional;

    using static Optional.Option;

    using Req = GetDataSetsRequest;
    using Res = GetDataSetsResponse;

    public class GetDataSetsCommand : ICommand<Req, Res, Exception>
    {
        private readonly IRepository repository;

        public GetDataSetsCommand(IRepository repository)
        {
            this.repository = repository;
        }

        public Option<Res, Exception> Execute(Req req)
        {
            try
            {
                return None<Res, Exception>(new NotImplementedException());
            }
            catch(Exception ex)
            {
                return None<Res, Exception>(ex);
            }
        }
    }
}
