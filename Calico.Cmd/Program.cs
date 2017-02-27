namespace Calico.Cmd
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var files = new Dictionary<string, string>
            {
                ["Anemoon"] = @"D:\temp\voorbeelddata\Anemoon.shp",
                ["Dahlia"] = @"D:\temp\voorbeelddata\Dahlia.shp",
                ["Iris"] = @"D:\temp\voorbeelddata\Iris.shp",
                ["Jasmijn"] = @"D:\temp\voorbeelddata\Jasmijn.shp",
                ["Lelie"] = @"D:\temp\voorbeelddata\Lelie.shp",
                ["Papaver"] = @"D:\temp\voorbeelddata\Papaver.shp",
                ["Petunia"] = @"D:\temp\voorbeelddata\Petunia.shp",
                ["Roos"] = @"D:\temp\voorbeelddata\Roos.shp",
                ["Sering"] = @"D:\temp\voorbeelddata\Sering.shp",
                ["Viool"] = @"D:\temp\voorbeelddata\Viool.shp",
            };

            foreach(var x in files)
            {
                InsertPlot(x.Key, x.Value);
            }
        }

        private static void InsertPlot(string name, string file)
        {

            var builder = new SqlConnectionStringBuilder
            {
                ["Data Source"] = @".\SQLEXPRESS",
                ["Initial Catalog"] = "Calico",
                ["Integrated Security"] = "SSPI",
            };

            using (var conn = new SqlConnection(builder.ConnectionString))
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    var repo = new SqlRepository(conn, tx);
                    var cmd = new NewPlotCommand(repo);

                    var req = new NewPlotRequest
                    {
                        ClientId = 1,
                        PathToShapefile = file,
                        Name = name,
                        SRID = 4326,
                    };

                    var res = cmd.Execute(req);

                    res.MatchSome(x =>
                    {
                        tx.Commit();
                    });

                    res.MatchNone(x =>
                    {
                        tx.Rollback();
                        Console.WriteLine(x);
                    });
                }
            }
        }
    }
}
