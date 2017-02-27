namespace Calico.Cmd
{
    using System;
    using System.Data.SqlClient;
    using AutoMapper;
    using Serilog;

    public class NewDataSetAction : IAction<NewDataSetArgs>
    {
        private readonly Func<SqlConnection> connectionFactory;

        public NewDataSetAction(Func<SqlConnection> connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public void Execute(NewDataSetArgs args)
        {
            using (var conn = this.connectionFactory())
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    var repo = new SqlRepository(conn, tx);
                    var cmd = new NewDataSetCommand(repo);
                    var req = Mapper.Map<NewDataSetRequest>(args);
                    var res = cmd.Execute(req);

                    res.MatchSome(x =>
                    {
                        tx.Commit();
                        Log.Information(
                            "Created data set {DataSetName} with id {DataSetId}",
                            x.DataSet.Name,
                            x.DataSet.Id);
                    });

                    res.MatchNone(x =>
                    {
                        tx.Rollback();
                        Log.Error(
                            x,
                            "Failed to create data set {DataSetName} for plot {PlotId}",
                            req.Name,
                            req.PlotId);
                    });
                }
            }
        }
    }
}
