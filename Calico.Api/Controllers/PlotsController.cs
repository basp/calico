namespace Calico.Api.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using AutoMapper;
    using Models;

    [RoutePrefix("api/plots")]
    public class PlotsController : ApiController
    {
        private readonly IRepository repository;

        public PlotsController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public PlotModel Get([FromUri] int id)
        {
            var rec = this.repository.GetPlot(id);
            return Mapper.Map<PlotModel>(rec);
        }

        [HttpGet]
        public IEnumerable<PlotModel> Get(
            [FromUri] int tenantId,
            [FromUri] int first = 100)
        {
            var recs = this.repository.GetPlots(tenantId, first);
            return Mapper.Map<IEnumerable<PlotModel>>(recs);
        }

        [HttpGet]
        [Route("{plotId}/datasets")]
        public IEnumerable<DataSetModel> GetDataSets(
            [FromUri] int plotId,
            [FromUri] int first = 100)
        {
            var recs = this.repository.GetDataSets(plotId, first);
            return Mapper.Map<IEnumerable<DataSetModel>>(recs);
        }
    }
}