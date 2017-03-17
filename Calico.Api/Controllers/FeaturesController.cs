namespace Calico.Api.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using AutoMapper;
    using Models;

    public class FeaturesController : ApiController
    {
        private readonly IRepository repository;

        public FeaturesController(IRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<FeatureModel> Get(
            [FromUri] int dataSetId,
            [FromUri] int first = 100)
        {
            var recs = this.repository.GetFeatures(dataSetId);
            return Mapper.Map<IEnumerable<FeatureModel>>(recs);
        }
    }
}