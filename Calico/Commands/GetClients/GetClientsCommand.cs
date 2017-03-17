// <copyright file="GetClientsCommand.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
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
                var clients = this.repository.GetClients(first: req.Top, after: 0);
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
