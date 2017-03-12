// <copyright file="Program.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Web
{
    using System.IO;
    using AutoMapper;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Models;

    public class Program
    {
        public static void Main(string[] args)
        {
            Mapper.Initialize(x =>
            {
                x.CreateMap<ClientRecord, ClientModel>();
                x.CreateMap<DataTypeRecord, DataTypeModel>();
                x.CreateMap<DataSetRecord, DataSetModel>();
                x.CreateMap<FeatureTypeRecord, FeatureTypeModel>();
                x.CreateMap<PlotRecord, PlotModel>();
            });

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            host.Run();
        }
    }
}