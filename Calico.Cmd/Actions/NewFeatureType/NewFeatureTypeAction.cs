namespace Calico.Cmd
{
    using System;
    using System.Data.SqlClient;
    using AutoMapper;
    using Serilog;

    public class NewFeatureTypeAction : IAction<NewFeatureTypeArgs>
    {
        private readonly Func<SqlConnection> connectionFactory;

        public NewFeatureTypeAction(Func<SqlConnection> connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public void Execute(NewFeatureTypeArgs args)
        {
            using (var conn = this.connectionFactory())
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    var repo = new SqlRepository(conn, tx);
                    var cmd = new NewFeatureTypeCommand(repo);
                    var req = Mapper.Map<NewFeatureTypeRequest>(args);
                    var res = cmd.Execute(req);

                    res.MatchSome(x =>
                    {
                        tx.Commit();
                        Log.Information(
                            "Created new feature type {FeatureTypeName} with id {FeatureTypeId}",
                            x.FeatureType.Name,
                            x.FeatureType.Id);

                        const string SuggestedCommand = "ImportAttributes";
                        Log.Information(
                            "You'll need to {SuggestedCommand} to finalize {FeatureTypeName}", 
                            SuggestedCommand,
                            req.Name);
                    });

                    res.MatchNone(x =>
                    {
                        tx.Rollback();
                        Log.Error(
                            x,
                            "Failed to create feature type {FeatureTypeName} for client {ClientId}",
                            req.Name,
                            req.ClientId);
                    });
                }
            }
        }
    }
}
