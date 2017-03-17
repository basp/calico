// <copyright file="DataSetsController.cs" company="TMG">
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
    public class DataSetsController : Controller
    {
        private readonly CalicoContext context;

        public DataSetsController(CalicoContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<DataSetModel> Get()
        {
            var dataSets = this.context.DataSets
                .ToList();

            return dataSets.Select(x => Mapper.Map<DataSetModel>(x));
        }

        [HttpGet]
        [Route("{id}")]
        public DataSetModel Get(int id)
        {
            var dataSet = this.context.DataSets
                .Single(x => x.Id == id);

            return Mapper.Map<DataSetModel>(dataSet);
        }

        [HttpGet]
        [Route("plot/{plotId}")]
        public IEnumerable<DataSetModel> GetByPlot(int plotId)
        {
            throw new NotImplementedException();
        }
    }
}
