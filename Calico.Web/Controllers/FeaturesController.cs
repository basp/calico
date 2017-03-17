// <copyright file="FeaturesController.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    [Route("api/[controller]")]
    public class FeaturesController : Controller
    {
        private readonly IRepository repository;
        private readonly ISession session;

        public FeaturesController(ISession session, IRepository repository)
        {
            this.repository = repository;
            this.session = session;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<PlotModel> Get(int dataSetId)
        {
            throw new NotImplementedException();
        }
    }
}