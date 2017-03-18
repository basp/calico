namespace Calico.Api.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using AutoMapper;
    using Models;

    [RoutePrefix("api/tenants")]
    public class TenantsController : ApiController
    {
        private readonly IRepository repository;

        public TenantsController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public TenantModel Get([FromUri] int id)
        {
            var rec = this.repository.GetTenant(id);
            return Mapper.Map<TenantModel>(rec);
        }

        [HttpGet]
        public IEnumerable<TenantModel> GetAll([FromUri] int first = 100)
        {
            var recs = this.repository.GetTenants(first, after: 0);
            return Mapper.Map<IEnumerable<TenantModel>>(recs);
        }

        [HttpGet]
        [Route("{tenantId}/plots")]
        public IEnumerable<PlotModel> GetPlots(
            [FromUri] int tenantId,
            [FromUri] int first = 100)
        {
            var recs = this.repository.GetPlots(tenantId, first);
            return Mapper.Map<IEnumerable<PlotModel>>(recs);
        }

        [HttpGet]
        [Route("{tenantId}/featuretypes")]
        public IEnumerable<FeatureTypeModel> GetFeatureTypes(
            [FromUri] int tenantId,
            [FromUri] int first = 100)
        {
            var recs = this.repository.GetFeatureTypes(tenantId, first);
            return Mapper.Map<IEnumerable<FeatureTypeModel>>(recs);
        }
    }
}
