﻿// <copyright file="GetDataTypesAction.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using System;
    using System.Data.SqlClient;
    using AutoMapper;
    using Newtonsoft.Json;
    using Serilog;

    public class GetDataTypesAction : IAction<GetDataTypesArgs>
    {
        private readonly Func<SqlConnection> connectionFactory;

        public GetDataTypesAction(Func<SqlConnection> connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public void Execute(GetDataTypesArgs args)
        {
            using (var conn = this.connectionFactory())
            {
                var repo = new SqlRepository(conn);
                var cmd = new GetDataTypesCommand(repo);
                var req = Mapper.Map<GetDataTypesRequest>(args);
                var res = cmd.Execute(req);

                res.MatchSome(x =>
                {
                    var json = JsonConvert.SerializeObject(x.DataTypes);
                    Console.WriteLine(json);
                });

                res.MatchNone(x =>
                {
                    Log.Error(x, "Could not get data types");
                });
            }
        }
    }
}
