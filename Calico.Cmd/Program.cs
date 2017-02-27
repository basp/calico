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
        public void ImportFeatures(ImportFeaturesArgs args) =>
            new ImportFeaturesAction(ConnectionFactory).Execute(args);

        [ArgActionMethod]
        public void NewClient(NewClientArgs args) =>
            new NewClientAction(ConnectionFactory).Execute(args);

        [ArgActionMethod]
        public void NewDataSet(NewDataSetArgs args) =>
            new NewDataSetAction(ConnectionFactory).Execute(args);

        [ArgActionMethod]
        public void NewPlot(NewPlotArgs args) =>
            new NewPlotAction(ConnectionFactory).Execute(args);

        private static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.LiterateConsole()
                .CreateLogger();

            Mapper.Initialize(x =>
            {
                x.CreateMap<NewPlotArgs, NewPlotRequest>();
                x.CreateMap<NewClientArgs, NewClientRequest>();
                x.CreateMap<NewDataSetArgs, NewDataSetRequest>()
                    .ForMember(dest => dest.DateCreated, opt => opt.UseValue(DateTime.UtcNow));
                x.CreateMap<ImportFeaturesArgs, ImportFeaturesRequest>();
            });

            Args.InvokeAction<Program>(args);
        }
    }
}
