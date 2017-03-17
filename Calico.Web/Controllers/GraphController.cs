// <copyright file="GraphController.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    [Route("api/[controller]")]
    public class GraphController : Controller
    {
        private readonly IRepository repository;
        private readonly ISession session;

        public GraphController(ISession session, IRepository repository)
        {
            this.session = session;
            this.repository = repository;
        }

        [HttpGet]
        public IEnumerable<dynamic> Get()
        {
            throw new NotImplementedException();
        }
    }
}
