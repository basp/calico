// <copyright file="NewDataTypeAction.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using System;
    using System.Data.SqlClient;
    using AutoMapper;
    using Serilog;

    public class NewDataTypeAction : IAction<NewDataTypeArgs>
    {
        private readonly Func<SqlConnection> connectionFactory;

        public NewDataTypeAction(Func<SqlConnection> connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public void Execute(NewDataTypeArgs args)
        {
            using (var conn = this.connectionFactory())
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    var repo = new SqlRepository(conn, tx);
                    var cmd = new NewDataTypeCommand(repo);
                    var req = Mapper.Map<NewDataTypeRequest>(args);
                    var res = cmd.Execute(req);

                    res.MatchSome(x =>
                    {
                        tx.Commit();
                        Log.Information(
                            "Created data type {DataTypeName} with id {DataTypeId}",
                            x.DataType.Name,
                            x.DataType.Id);
                    });

                    res.MatchNone(x =>
                    {
                        tx.Rollback();
                        Log.Error(
                            x,
                            "Could not create data type {DataTypeName}",
                            req.Name);
                    });
                }
            }
        }
    }
}
