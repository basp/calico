// <copyright file="Startup.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Web
{
    using System.Configuration;
    using System.Data.SqlClient;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using SimpleInjector;
    using SimpleInjector.Integration.AspNetCore.Mvc;
    using SimpleInjector.Lifestyles;

    public class Startup
    {
        private readonly Container container = new Container();

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            this.Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IControllerActivator>(
                new SimpleInjectorControllerActivator(this.container));

            services.UseSimpleInjectorAspNetRequestScoping(this.container);

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(this.Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();

            this.container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            this.InitializeContainer(app);
            this.container.Verify();
        }

        private void InitializeContainer(IApplicationBuilder app)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["dev"].ConnectionString;

            this.container.RegisterMvcControllers(app);
            this.container.RegisterSingleton(
                app.ApplicationServices.GetService<ILoggerFactory>());

            this.container.Register(
                () => new SqlConnection(connectionString),
                Lifestyle.Scoped);

            this.container.Register<ISession>(
                () => SqlSession.Open(this.container.GetInstance<SqlConnection>()),
                Lifestyle.Scoped);

            this.container.Register<IRepository, SqlRepository>(
                Lifestyle.Scoped);
        }
    }
}
