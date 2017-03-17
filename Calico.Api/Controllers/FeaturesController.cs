namespace Calico.Api.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using AutoMapper;
    using Calico.Data;
    using Calico.Api.Models;

    public class FeaturesController : ApiController
    {
        private readonly CalicoContext context;

        public FeaturesController(CalicoContext context)
        {
            this.context = context;
        }

        public IEnumerable<FeatureModel> Get([FromUri] int dataSetId)
        {
            var features = this.context.Features
                .Where(x => x.DataSetId == dataSetId)
                .OrderBy(x => x.Index)
                .ToList();

            return features.Select(x => Mapper.Map<FeatureModel>(x));
        }

        public FeatureModel Get([FromUri] int dataSetId, [FromUri] int index)
        {
            var feature = this.context.Features
                .Single(x => x.DataSetId == dataSetId && x.Index == index);

            return Mapper.Map<FeatureModel>(feature);
        }
    }
}