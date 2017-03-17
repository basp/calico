namespace Calico.Api.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using AutoMapper;
    using Models;

    [RoutePrefix("api/clients")]
    public class ClientsController : ApiController
    {
        private readonly IRepository repository;

        public ClientsController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ClientModel Get([FromUri] int id)
        {
            var rec = this.repository.GetClient(id);
            return Mapper.Map<ClientModel>(rec);
        }

        [HttpGet]
        public IEnumerable<ClientModel> GetAll([FromUri] int first = 100)
        {
            var recs = this.repository.GetClients(first);
            return Mapper.Map<IEnumerable<ClientModel>>(recs);
        }

        [HttpGet]
        [Route("{clientId}/plots")]
        public IEnumerable<PlotModel> GetPlots(
            [FromUri] int clientId,
            [FromUri] int first = 100)
        {
            var recs = this.repository.GetPlots(clientId, first);
            return Mapper.Map<IEnumerable<PlotModel>>(recs);
        }

        [HttpGet]
        [Route("{clientId}/featuretypes")]
        public IEnumerable<FeatureTypeModel> GetFeatureTypes(
            [FromUri] int clientId,
            [FromUri] int first = 100)
        {
            var recs = this.repository.GetFeatureTypes(clientId, first);
            return Mapper.Map<IEnumerable<FeatureTypeModel>>(recs);
        }
    }
}
