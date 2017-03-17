// <copyright file="GraphController.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Web.Controllers
{
    using System;
    using System.Net;
    using System.Net.Http;
    using Microsoft.AspNetCore.Mvc;

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
        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
