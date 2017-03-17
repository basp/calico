// <copyright file="FeatureTypesController.cs" company="TMG">
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
    public class FeatureTypesController : Controller
    {
        private readonly CalicoContext context;

        public FeatureTypesController(CalicoContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("client/{clientId}")]
        public IEnumerable<FeatureTypeModel> GetByClient(int clientId)
        {
            var featureTypes = this.context.FeatureTypes
                .Where(x => x.ClientId == clientId)
                .ToList();

            return featureTypes.Select(x => Mapper.Map<FeatureTypeModel>(x));
        }

        [HttpGet]
        public IEnumerable<FeatureTypeModel> Get()
        {
            var featureTypes = this.context.FeatureTypes
                .ToList();

            return featureTypes.Select(x => Mapper.Map<FeatureTypeModel>(x));
        }

        [HttpGet("{id}")]
        public FeatureTypeModel Get(int id)
        {
            var featureType = this.context.FeatureTypes
                .Single(x => x.Id == id);

            return Mapper.Map<FeatureTypeModel>(featureType);
        }
    }
}