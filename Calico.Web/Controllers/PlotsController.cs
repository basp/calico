﻿// <copyright file="PlotsController.cs" company="TMG">
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
    public class PlotsController : Controller
    {
        private readonly IRepository repository;
        private readonly ISession session;

        public PlotsController(ISession session, IRepository repository)
        {
            this.repository = repository;
            this.session = session;
        }

        [HttpGet]
        public IEnumerable<PlotModel> Get()
        {
            var cmd = new GetPlotsCommand(this.repository);
            var req = new GetPlotsRequest
            {
                ClientId = 1,
                Top = 100,
            };

            var res = cmd.Execute(req);
            return res.Match(
                some => some.Plots.Select(x => Mapper.Map<PlotModel>(x)),
                none => throw none);
        }

        [HttpGet("{id}")]
        public PlotModel Get(int id)
        {
            var rec = this.repository.GetPlot(id);
            return new PlotModel
            {
                Id = rec.Id,
                ClientId = rec.ClientId,
                Name = rec.Name,
                Uri = string.Empty,
            };
        }
    }
}