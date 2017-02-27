namespace Calico.Cmd
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using PowerArgs;
    using Serilog;
    using AutoMapper;

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

        [ArgActionMethod]
        public void ImportFeatures(ImportFeaturesArgs args) =>
            new ImportFeaturesAction(ConnectionFactory).Execute(args);

        [ArgActionMethod]
        public void NewClient(NewClientArgs args) =>
            new NewClientAction(ConnectionFactory).Execute(args);

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
                x.CreateMap<ImportFeaturesArgs, ImportFeaturesRequest>();
            });
        }
    }
}
