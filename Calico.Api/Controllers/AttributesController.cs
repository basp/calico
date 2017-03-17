namespace Calico.Api.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using Models;
    using AutoMapper;

    public class AttributesController : ApiController
    {
        private readonly IRepository repository;

        public AttributesController(IRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<AttributeModel> Get(
            [FromUri] int featureTypeId,
            [FromUri] int first = 100)
        {
            var recs = this.repository.GetAttributes(featureTypeId);
            return Mapper.Map<IEnumerable<AttributeModel>>(recs);
        }
    }
}