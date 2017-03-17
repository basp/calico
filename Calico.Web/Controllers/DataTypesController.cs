// <copyright file="DataTypesController.cs" company="TMG">
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
    public class DataTypesController : Controller
    {
        private readonly CalicoContext context;

        public DataTypesController(CalicoContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<DataTypeModel> Get()
        {
            var dataTypes = this.context.DataTypes
                .ToList();

            return dataTypes.Select(x => Mapper.Map<DataTypeModel>(x));
        }

        [HttpGet]
        public DataTypeModel Get(int id)
        {
            var dataType = this.context.DataTypes
                .Single(x => x.Id == id);

            return Mapper.Map<DataTypeModel>(dataType);
        }
    }
}
