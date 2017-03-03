// <copyright file="GetClientsAction.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using System;
    using System.Data.SqlClient;
    using AutoMapper;
    using Newtonsoft.Json;
    using Serilog;

    public class GetClientsAction : IAction<GetClientsArgs>
    {
        private readonly Func<SqlConnection> connectionFactory;

        public GetClientsAction(Func<SqlConnection> connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public void Execute(GetClientsArgs args)
        {
            using (var conn = this.connectionFactory())
            {
                var repo = new SqlRepository(conn);
                var cmd = new GetClientsCommand(repo);
                var req = Mapper.Map<GetClientsRequest>(args);
                var res = cmd.Execute(req);

                res.MatchSome(x =>
                {
                    var json = JsonConvert.SerializeObject(x.Clients);
                    Console.WriteLine(json);
                });

                res.MatchNone(x =>
                {
                    Log.Error(x, "Could not get clients");
                });
            }
        }
    }
}
