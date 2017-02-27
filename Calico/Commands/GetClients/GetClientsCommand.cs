﻿namespace Calico
{
    using System;
    using Optional;

    using static Optional.Option;

    using Req = GetClientsRequest;
    using Res = GetClientsResponse;

    public class GetClientsCommand : ICommand<Req, Res, Exception>
    {
        private readonly IRepository repository;

        public GetClientsCommand(IRepository repository)
        {
            this.repository = repository;
        }

        public Option<Res, Exception> Execute(Req req)
        {
            try
            {
                var clients = this.repository.GetClients(req.Top);
                var res = new Res { Clients = clients };
                return Some<Res, Exception>(res);
            }
            catch (Exception ex)
            {
                return None<Res, Exception>(ex);
            }
        }
    }
}
