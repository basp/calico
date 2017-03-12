// <copyright file="FeatureTypesController.cs" company="TMG">
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
    public class FeatureTypesController : Controller
    {
        private readonly IRepository repository;
        private readonly ISession session;

        public FeatureTypesController(ISession session, IRepository repository)
        {
            this.repository = repository;
            this.session = session;
        }

        [HttpGet]
        public IEnumerable<FeatureTypeModel> Get()
        {
            var cmd = new GetFeatureTypesCommand(this.repository);
            var req = new GetFeatureTypesRequest
            {
                ClientId = 1,
                Top = 100,
            };

            var res = cmd.Execute(req);
            return res.Match(
                some => some.FeatureTypes.Select(x => Mapper.Map<FeatureTypeModel>(x)),
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