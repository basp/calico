namespace Calico.Api.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using AutoMapper;
    using Models;

    [RoutePrefix("api/datasets")]
    public class DataSetsController : ApiController
    {
        private readonly IRepository repository;

        public DataSetsController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IEnumerable<DataSetModel> Get(
            [FromUri] int plotId,
            [FromUri] int first = 100)
        {
            var recs = this.repository.GetDataSets(plotId, first);
            return Mapper.Map<IEnumerable<DataSetModel>>(recs);
        }

        [HttpGet]
        [Route("{dataSetId}/featuretype")]
        public FeatureTypeModel GetFeatureType([FromUri] int dataSetId)
        {
            var dataSet = this.repository.GetDataSet(dataSetId);
            var featureType = this.repository.GetFeatureType(dataSet.FeatureTypeId);
            return Mapper.Map<FeatureTypeModel>(featureType);
        }

        [HttpGet]
        [Route("{dataSetId}/features")]
        public IEnumerable<FeatureModel> GetFeatures(
            [FromUri] int dataSetId,
            [FromUri] int first = 100)
        {
            var recs = this.repository.GetFeatures(dataSetId);
            return Mapper.Map<IEnumerable<FeatureModel>>(recs);
        }
    }
}