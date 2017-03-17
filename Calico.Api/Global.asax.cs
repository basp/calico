namespace Calico.Api
{
    using System.Data.SqlClient;
    using System.Web;
    using System.Web.Http;
    using AutoMapper;
    using Models;
    using SimpleInjector;
    using SimpleInjector.Lifestyles;
    using SimpleInjector.Integration.WebApi;
    using Graph;
    using Graph.Types;
    using GraphQL.Types;

    public class WebApiApplication : HttpApplication
    {
        private const string ConnectionString =
            @"Data Source=.\SQLEXPRESS;Initial Catalog=Calico;Integrated Security=SSPI";

        private static void InitializeMapper()
        {
            Mapper.Initialize(x =>
            {
                x.CreateMap<AttributeRecord, AttributeModel>();
                x.CreateMap<ClientRecord, ClientModel>();
                x.CreateMap<DataSetRecord, DataSetModel>();
                x.CreateMap<DataTypeRecord, DataTypeModel>();
                x.CreateMap<FeatureRecord, FeatureModel>()
                    .ForMember(dest => dest.Geometry, opt => opt.MapFrom(src => src.Wkt));
                x.CreateMap<FeatureTypeRecord, FeatureTypeModel>();
                x.CreateMap<PlotRecord, PlotModel>()
                    .ForMember(dest => dest.Geometry, opt => opt.MapFrom(src => src.Wkt));
            });
        }

        private static Container InitializeContainer()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            container.Register(
                () => new CalicoSchema(
                    x => (GraphType)container.GetInstance(x)),
                Lifestyle.Scoped);

            container.Register<ISession>(
                () => SqlSession.Open(new SqlConnection(ConnectionString)),
                Lifestyle.Scoped);

            container.Register<IRepository>(
                () => new SqlRepository(container.GetInstance<ISession>()),
                Lifestyle.Scoped);

            container.Register<Attribute>(Lifestyle.Scoped);
            container.Register<AttributeValue>(Lifestyle.Scoped);
            container.Register<Client>(Lifestyle.Scoped);
            container.Register<DataSet>(Lifestyle.Scoped);
            container.Register<DataType>(Lifestyle.Scoped);
            container.Register<Feature>(Lifestyle.Scoped);
            container.Register<FeatureType>(Lifestyle.Scoped);
            container.Register<Plot>(Lifestyle.Scoped);

            container.RegisterWebApiControllers(
                GlobalConfiguration.Configuration);

            return container;
        }

        protected void Application_Start()
        {
            InitializeMapper();

            var container = InitializeContainer();
            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}