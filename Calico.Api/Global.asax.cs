namespace Calico.Api
{
    using System.Web;
    using System.Web.Http;
    using AutoMapper;
    using Calico.Api.Models;
    using Calico.Data;
    using SimpleInjector;
    using SimpleInjector.Lifestyles;
    using SimpleInjector.Integration.WebApi;

    public class WebApiApplication : HttpApplication
    {
        private const string ConnectionString =
            @"Data Source=.\SQLEXPRESS;Initial Catalog=Calico;Integrated Security=SSPI";

        protected void Application_Start()
        {
            Mapper.Initialize(x =>
            {
                x.CreateMap<Client, ClientModel>();
                x.CreateMap<Feature, FeatureModel>()
                    .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Geometry.AsText()));
            });

            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            container.Register(
                () => new CalicoContext(ConnectionString), Lifestyle.Scoped);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}