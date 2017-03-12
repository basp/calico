// <copyright file="DataTypesController.cs" company="TMG">
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
    public class DataTypesController : Controller
    {
        private readonly IRepository repository;
        private readonly ISession session;

        public DataTypesController(ISession session, IRepository repository)
        {
            this.session = session;
            this.repository = repository;
        }

        [HttpGet]
        public IEnumerable<DataTypeModel> Get()
        {
            var cmd = new GetDataTypesCommand(this.repository);
            var req = new GetDataTypesRequest();
            var res = cmd.Execute(req);

            return res.Match(
                some => some.DataTypes.Select(x => Mapper.Map<DataTypeModel>(x)),
                none => throw none);
        }
    }
}
