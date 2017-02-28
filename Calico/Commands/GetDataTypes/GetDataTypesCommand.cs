// <copyright file="GetDataTypesCommand.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using Optional;

    using static Optional.Option;

    using Req = GetDataTypesRequest;
    using Res = GetDataTypesResponse;

    public class GetDataTypesCommand : ICommand<Req, Res, Exception>
    {
        private readonly IRepository repository;

        public GetDataTypesCommand(IRepository repository)
        {
            this.repository = repository;
        }

        public Option<Res, Exception> Execute(Req req)
        {
            try
            {
                var dataTypes = this.repository.GetDataTypes();
                var res = new Res { DataTypes = dataTypes };
                return Some<Res, Exception>(res);
            }
            catch (Exception ex)
            {
                return None<Res, Exception>(ex);
            }
        }
    }
}
