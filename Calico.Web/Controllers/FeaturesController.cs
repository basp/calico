// <copyright file="FeaturesController.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Calico.Data;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    [Route("api/[controller]")]
    public class FeaturesController : Controller
    {
        private readonly CalicoContext context;

        public FeaturesController(CalicoContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("dataset/{dataSetId}")]
        public IEnumerable<FeatureModel> Get(int dataSetId)
        {
            var features = this.context.Features
                .Where(x => x.DataSetId == dataSetId)
                .ToList();

            return features.Select(x => Mapper.Map<FeatureModel>(x));
        }
    }
}