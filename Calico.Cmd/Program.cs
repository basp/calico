namespace Calico.Cmd
{
    using System;
    using System.Data.SqlClient;
    using AutoMapper;
    using PowerArgs;
    using Serilog;

    [ArgExceptionBehavior(ArgExceptionPolicy.StandardExceptionHandling)]
    internal class Program
    {
        private static SqlConnectionStringBuilder ConnectionStringBuilder =
            new SqlConnectionStringBuilder
            {
                ["Data Source"] = @".\SQLEXPRESS",
                ["Initial Catalog"] = "Calico",
                ["Integrated Security"] = "SSPI",
            };

        private static Func<SqlConnection> ConnectionFactory =
            () => new SqlConnection(ConnectionStringBuilder.ConnectionString);

        [HelpHook]
        public bool Help { get; set; }

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
        [ArgDescription("Imports features from a shapefile")]
        public void ImportFeatures(ImportFeaturesArgs args) =>
            new ImportFeaturesAction(ConnectionFactory).Execute(args);

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

        private static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.LiterateConsole()
                .MinimumLevel.Debug()
                .CreateLogger();

            Mapper.Initialize(x =>
            {
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
                x.CreateMap<ImportFeaturesArgs, ImportFeaturesRequest>();
            });

            Args.InvokeAction<Program>(args);
        }
    }
}
