namespace Calico.Api
{
    using System.Web.Http;
    using System.Web.OData.Extensions;
    using Calico.Data;
    using Microsoft.Restier.Providers.EntityFramework;
    using Microsoft.Restier.Publishers.OData;
    using Microsoft.Restier.Publishers.OData.Batch;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Filter().Expand().Select().OrderBy().MaxTop(null).Count();
            config.MapRestierRoute<EntityFrameworkApi<CalicoContext>>(
                "CalicoContext",
                "api/odata",
                new RestierBatchHandler(GlobalConfiguration.DefaultServer));

            /*
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            */
        }
    }
}
