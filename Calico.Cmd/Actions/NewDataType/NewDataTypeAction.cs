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
            using (var session = SqlSession.Open(conn))
            {
                var repo = new SqlRepository(session);
                var cmd = new NewDataTypeCommand(repo);
                var req = Mapper.Map<NewDataTypeRequest>(args);
                var res = cmd.Execute(req);

                res.MatchSome(x =>
                {
                    session.Commit();
                    Log.Information(
                        "Created data type {DataTypeName} with id {DataTypeId}",
                        x.DataType.Name,
                        x.DataType.Id);
                });

                res.MatchNone(x =>
                {
                    session.Rollback();
                    Log.Error(
                        x,
                        "Could not create data type {DataTypeName}",
                        req.Name);
                });
            }
        }
    }
}
