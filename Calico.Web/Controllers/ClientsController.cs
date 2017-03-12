// <copyright file="ClientsController.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    [Route("api/[controller]")]
    public class ClientsController : Controller
    {
        private readonly IRepository repository;
        private readonly ISession session;

        public ClientsController(ISession session, IRepository repository)
        {
            this.session = session;
            this.repository = repository;
        }

        [HttpGet]
        public IEnumerable<ClientModel> Get()
        {
            var cmd = new GetClientsCommand(this.repository);
            var req = new GetClientsRequest
            {
                Top = 100,
            };

            var res = cmd.Execute(req);

            return res.Match(
                some => some.Clients.Select(x => Mapper.Map<ClientModel>(x)),
                none => throw none);
        }

        [HttpGet("{id}")]
        public ClientModel Get(int id)
        {
            var client = this.repository.GetClient(id);
            return Mapper.Map<ClientModel>(client);
        }
    }
}
