namespace Calico.Cmd
{
    using AutoMapper;
    using Serilog;
    using System;
    using System.Data.SqlClient;

    public class NewPlotAction : IAction<NewPlotArgs>
    {
        private readonly Func<SqlConnection> connectionFactory;

        public NewPlotAction(Func<SqlConnection> connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public void Execute(NewPlotArgs args)
        {
            using (var conn = this.connectionFactory())
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    var repo = new SqlRepository(conn, tx);
                    var cmd = new NewPlotCommand(repo);
                    var req = Mapper.Map<NewPlotRequest>(args);
                    var res = cmd.Execute(req);

                    res.MatchSome(x =>
                    {
                        tx.Commit();
                        Log.Information(
                            "Created plot {PlotName} with id {PlotId}",
                            x.Plot.Name,
                            x.Plot.Id);
                    });

                    res.MatchNone(x =>
                    {
                        tx.Rollback();
                        Log.Error(
                            x,
                            "Failed to create plot from shapefile {Shapefile}",
                            req.PathToShapefile);
                    });
                }
            }
        }
    }
}
