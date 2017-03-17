namespace Calico.Api.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using AutoMapper;
    using Models;

    public class FeatureTypesController : ApiController
    {
        private readonly IRepository repository;

        public FeatureTypesController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IEnumerable<FeatureTypeModel> Get(
            [FromUri] int clientId,
            [FromUri] int first = 100)
        {
            var recs = this.repository.GetFeatureTypes(clientId, first);
            return Mapper.Map<IEnumerable<FeatureTypeModel>>(recs);
        }
    }
}