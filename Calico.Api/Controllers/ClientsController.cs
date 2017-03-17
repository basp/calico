namespace Calico.Api.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using AutoMapper;
    using Calico.Api.Models;
    using Calico.Data;

    public class ClientsController : ApiController
    {
        private readonly CalicoContext context;

        public ClientsController(CalicoContext context)
        {
            this.context = context;
        }

        public ClientModel Get(int id)
        {
            var client = this.context.Clients.Single(x => x.Id == id);
            return Mapper.Map<ClientModel>(client);
        }

        public IEnumerable<ClientModel> Get()
        {
            var clients = this.context.Clients.ToList();
            return clients.Select(x => Mapper.Map<ClientModel>(x));
        }
    }
}
