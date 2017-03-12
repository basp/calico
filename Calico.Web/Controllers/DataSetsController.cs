// <copyright file="DataSetsController.cs" company="TMG">
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
    public class DataSetsController : Controller
    {
        private readonly ISession session;
        private readonly IRepository repository;

        public DataSetsController(ISession session, IRepository repository)
        {
            this.session = session;
            this.repository = repository;
        }

        [HttpGet]
        public IEnumerable<DataSetModel> Get()
        {
            // So here we run into some real restrictions of our engine.
            // We would like to offer an ability to get all data sets but
            // the underlying engine doesn't allow it.
            // Expand existing capabilities or implement a new command?
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{id}")]
        public IEnumerable<DataSetModel> Get(int dataSetId)
        {
            var cmd = new GetDataSetCommand(this.repository);
            var req = new GetDataSetRequest
            {
                DataSetId = dataSetId,
            };

            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("plot/{plotId}")]
        public IEnumerable<DataSetModel> GetByPlot(int plotId)
        {
            var cmd = new GetDataSetsCommand(this.repository);
            var req = new GetDataSetsRequest
            {
                PlotId = plotId,
                Top = 100,
            };

            var res = cmd.Execute(req);
            return res.Match(
                some => some.DataSets.Select(x => Mapper.Map<DataSetModel>(x)),
                none => throw none);
        }
    }
}
