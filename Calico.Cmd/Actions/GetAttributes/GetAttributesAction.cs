// <copyright file="GetAttributesAction.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using System;
    using System.Data.SqlClient;
    using AutoMapper;
    using Newtonsoft.Json;
    using Serilog;

    public class GetAttributesAction : IAction<GetAttributesArgs>
    {
        private Func<SqlConnection> connectionFactory;

        public GetAttributesAction(Func<SqlConnection> connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public void Execute(GetAttributesArgs args)
        {
            using (var conn = this.connectionFactory())
            {
                var repo = new SqlRepository(conn);
                var cmd = new GetAttributesCommand(repo);
                var req = Mapper.Map<GetAttributesRequest>(args);
                var res = cmd.Execute(req);

                res.MatchSome(x =>
                {
                    var json = JsonConvert.SerializeObject(x.Attributes);
                    Console.WriteLine(json);
                });

                res.MatchNone(x =>
                {
                    Log.Error(
                        x,
                        "Could not get attributes for feature type {FeatureTypeId}",
                        req.FeatureTypeId);
                });
            }
        }
    }
}
