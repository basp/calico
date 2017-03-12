// <copyright file="PlotsController.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class PlotsController : Controller
    {
        private readonly IRepository repository;

        public PlotsController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            var plots = this.repository.GetPlots(1, 100);
            return plots.Select(x => x.Name);
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            var plot = this.repository.GetPlot(id);
            return plot.Name;
        }
    }
}