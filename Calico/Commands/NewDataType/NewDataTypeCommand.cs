// <copyright file="NewDataTypeCommand.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using Optional;

    using static Optional.Option;

    using Req = NewDataTypeRequest;
    using Res = NewDataTypeResponse;

    public class NewDataTypeCommand : ICommand<Req, Res, Exception>
    {
        private readonly IRepository repository;

        public NewDataTypeCommand(IRepository repository)
        {
            this.repository = repository;
        }

        public Option<Res, Exception> Execute(Req req)
        {
            try
            {
                var rec = this.InsertDataType(req.Name, req.SqlType, req.BclType);
                var res = new Res { DataType = rec };
                return Some<Res, Exception>(res);
            }
            catch (Exception ex)
            {
                return None<Res, Exception>(ex);
            }
        }

        private DataTypeRecord InsertDataType(string name, string sqlType, string bclType)
        {
            var rec = new DataTypeRecord
            {
                Name = name,
                SqlType = sqlType,
                BclType = bclType,
            };

            rec.Id = this.repository.InsertDataType(rec);
            return rec;
        }
    }
}
