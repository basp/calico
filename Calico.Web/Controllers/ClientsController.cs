// <copyright file="ClientsController.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Calico.Data;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    [Route("api/[controller]")]
    public class ClientsController : Controller
    {
        private readonly CalicoContext context;

        public ClientsController(CalicoContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<ClientModel> Get()
        {
            var clients = this.context.Clients
                .ToList();

            return clients.Select(x => Mapper.Map<ClientModel>(x));
        }

        [HttpGet("{id}")]
        public ClientModel Get(int id)
        {
            var client = this.context.Clients
                .Single(x => x.Id == id);

            return Mapper.Map<ClientModel>(client);
        }
    }
}
