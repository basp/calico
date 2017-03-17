namespace Calico.Api.Controllers
{
    using System.Text;
    using System.Threading.Tasks;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Graph;
    using GraphQL;
    using GraphQL.Http;

    public class GraphQLController : ApiController
    {
        private const string MediaType = "application/json";

        private readonly CalicoSchema schema;

        public GraphQLController(CalicoSchema schema)
        {
            this.schema = schema;
        }

        private void InitOptions(ExecutionOptions opts, string query)
        {
            opts.Schema = this.schema;
            opts.Query = $"query{query}";
        }

        [HttpGet]
        public async Task<HttpResponseMessage> Get([FromUri] string query)
        {
            var result = await new DocumentExecuter()
                .ExecuteAsync(x => InitOptions(x, query))
                .ConfigureAwait(false);

            var json = new DocumentWriter(indent: true).Write(result);
            var content = new StringContent(
                json,
                Encoding.UTF8,
                MediaType);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = content,
            };
        }
    }
}