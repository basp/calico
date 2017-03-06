// <copyright file="Program.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using System;
    using System.Configuration;
    using System.Data.SqlClient;
    using AutoMapper;
    using PowerArgs;
    using Serilog;

    [ArgExceptionBehavior(ArgExceptionPolicy.StandardExceptionHandling)]
    internal class Program
    {
        private static string env =
            ConfigurationManager.AppSettings.Get("env");

        private static string connectionString =
            ConfigurationManager.ConnectionStrings[env].ConnectionString;

        [HelpHook]
        public bool Help { get; set; }

        [ArgActionMethod]
        public void CategorizeDataSet(CategorizeDataSetArgs args) =>
            new CategorizeDataSetAction().Execute(args);

        [ArgActionMethod]
        [ArgDescription("Deletes a data set")]
        public void DeleteDataSet(DeleteDataSetArgs args) =>
            new DeleteDataSetAction(ConnectionFactory).Execute(args);

        [ArgActionMethod]
        [ArgDescription("Deletes a feature type")]
        public void DeleteFeatureType(DeleteFeatureTypeArgs args) =>
            new DeleteFeatureTypeAction(ConnectionFactory).Execute(args);

        [ArgActionMethod]
        [ArgDescription("Deletes a plot")]
        public void DeletePlot(DeletePlotArgs args) =>
            new DeletePlotAction(ConnectionFactory).Execute(args);

        [ArgActionMethod]
        [ArgDescription("Gets a list of attributes")]
        public void GetAttributes(GetAttributesArgs args) =>
            new GetAttributesAction(ConnectionFactory).Execute(args);

        [ArgActionMethod]
        [ArgDescription("Gets a list of clients")]
        public void GetClients(GetClientsArgs args) =>
            new GetClientsAction(ConnectionFactory).Execute(args);

        [ArgActionMethod]
        [ArgDescription("Gets a list of data sets")]
        public void GetDataSets(GetDataSetsArgs args) =>
            new GetDataSetsAction(ConnectionFactory).Execute(args);

        [ArgActionMethod]
        [ArgDescription("Gets a list of data types")]
        public void GetDataTypes(GetDataTypesArgs args) =>
            new GetDataTypesAction(ConnectionFactory).Execute(args);

        [ArgActionMethod]
        [ArgDescription("Gets a list of feature types")]
        public void GetFeatureTypes(GetFeatureTypesArgs args) =>
            new GetFeatureTypesAction(ConnectionFactory).Execute(args);

        [ArgActionMethod]
        [ArgDescription("Gets a list of plots")]
        public void GetPlots(GetPlotsArgs args) =>
            new GetPlotsAction(ConnectionFactory).Execute(args);

        [ArgActionMethod]
        [ArgDescription("Imports attributes from a shapefile")]
        public void ImportAttributes(ImportAttributesArgs args) =>
            new ImportAttributesAction(ConnectionFactory).Execute(args);

        [ArgActionMethod]
        [ArgDescription("Imports attribute values from a shapefile")]
        public void ImportAttributeValues(ImportAttributeValuesArgs args) =>
            new ImportAttributeValuesAction(ConnectionFactory).Execute(args);

        [ArgActionMethod]
        [ArgDescription("Import a data set from a shapefile")]
        public void ImportDataSet(ImportDataSetArgs args) =>
            new ImportDataSetAction(ConnectionFactory).Execute(args);

        [ArgActionMethod]
        [ArgDescription("Imports features from a shapefile")]
        public void ImportFeatures(ImportFeaturesArgs args) =>
            new ImportFeaturesAction(ConnectionFactory).Execute(args);

        [ArgActionMethod]
        [ArgDescription("Imports a feature type from a shapefile")]
        public void ImportFeatureType(ImportFeatureTypeArgs args) =>
            new ImportFeatureTypeAction(ConnectionFactory).Execute(args);

        [ArgActionMethod]
        [ArgDescription("Import a plot from a shapefile")]
        public void ImportPlot(ImportPlotArgs args) =>
            new ImportPlotAction(ConnectionFactory).Execute(args);

        [ArgActionMethod]
        [ArgDescription("Creates a new client")]
        public void NewClient(NewClientArgs args) =>
            new NewClientAction(ConnectionFactory).Execute(args);

        [ArgActionMethod]
        [ArgDescription("Creates a new data set")]
        public void NewDataSet(NewDataSetArgs args) =>
            new NewDataSetAction(ConnectionFactory).Execute(args);

        [ArgActionMethod]
        [ArgDescription("Creates a new data type")]
        public void NewDataType(NewDataTypeArgs args) =>
            new NewDataTypeAction(ConnectionFactory).Execute(args);

        [ArgActionMethod]
        [ArgDescription("Creates a new feature type")]
        public void NewFeatureType(NewFeatureTypeArgs args) =>
            new NewFeatureTypeAction(ConnectionFactory).Execute(args);

        [ArgActionMethod]
        [ArgDescription("Creates a new plot")]
        public void NewPlot(NewPlotArgs args) =>
            new NewPlotAction(ConnectionFactory).Execute(args);

        [ArgActionMethod]
        public void QuantifyDataSet(QuantifyDataSetArgs args) =>
            new QuantifyDataSetAction().Execute(args);

        [ArgActionMethod]
        [ArgDescription("Scans a shapefile for information")]
        public void ScanShapefile(ScanShapefileArgs args) =>
            new ScanShapefileAction(ConnectionFactory).Execute(args);

        private static SqlConnection ConnectionFactory() =>
            new SqlConnection(connectionString);

        private static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.LiterateConsole()
                .MinimumLevel.Debug()
                .CreateLogger();

            Mapper.Initialize(x =>
            {
                x.CreateMap<CategorizeDataSetArgs, CategorizeDataSetRequest>();
                x.CreateMap<DeleteDataSetArgs, DeleteDataSetRequest>();
                x.CreateMap<DeleteFeatureTypeArgs, DeleteFeatureTypeRequest>();
                x.CreateMap<DeletePlotArgs, DeletePlotRequest>();
                x.CreateMap<GetAttributesArgs, GetAttributesRequest>();
                x.CreateMap<GetClientsArgs, GetClientsRequest>();
                x.CreateMap<GetDataSetsArgs, GetDataSetsRequest>();
                x.CreateMap<GetDataTypesArgs, GetDataTypesRequest>();
                x.CreateMap<GetFeatureTypesArgs, GetFeatureTypesRequest>();
                x.CreateMap<GetPlotsArgs, GetPlotsRequest>();
                x.CreateMap<NewPlotArgs, NewPlotRequest>();
                x.CreateMap<NewClientArgs, NewClientRequest>();
                x.CreateMap<NewDataSetArgs, NewDataSetRequest>()
                    .ForMember(dest => dest.DateCreated, opt => opt.UseValue(DateTime.UtcNow));
                x.CreateMap<NewDataTypeArgs, NewDataTypeRequest>();
                x.CreateMap<NewFeatureTypeArgs, NewFeatureTypeRequest>();
                x.CreateMap<ImportAttributesArgs, ImportAttributesRequest>();
                x.CreateMap<ImportAttributeValuesArgs, ImportAttributeValuesRequest>();
                x.CreateMap<ImportDataSetArgs, ImportDataSetRequest>()
                    .ForMember(dest => dest.DateCreated, opt => opt.UseValue(DateTime.UtcNow));
                x.CreateMap<ImportFeaturesArgs, ImportFeaturesRequest>();
                x.CreateMap<ImportFeatureTypeArgs, ImportFeatureTypeRequest>();
                x.CreateMap<ImportPlotArgs, ImportPlotRequest>()
                    .ForMember(dest => dest.DateCreated, opt => opt.UseValue(DateTime.UtcNow));
                x.CreateMap<QuantifyDataSetArgs, QuantifyDataSetRequest>();
                x.CreateMap<ScanShapefileArgs, ScanShapefileRequest>();
            });

            Args.InvokeAction<Program>(args);
        }
    }
}
