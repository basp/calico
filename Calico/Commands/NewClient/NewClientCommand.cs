// <copyright file="NewClientCommand.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using Optional;

    using static Optional.Option;

    using Req = NewClientRequest;
    using Res = NewClientResponse;

    public class NewClientCommand : ICommand<Req, Res, Exception>
    {
        private readonly IRepository repository;

        public NewClientCommand(IRepository repository)
        {
            this.repository = repository;
        }

        public Option<Res, Exception> Execute(Req req)
        {
            try
            {
                var rec = this.InsertClient(req.Name);
                var res = new Res { Client = rec };
                return Some<Res, Exception>(res);
            }
            catch (Exception ex)
            {
                return None<Res, Exception>(ex);
            }
        }

        private ClientRecord InsertClient(string name)
        {
            var rec = new ClientRecord
            {
                Name = name,
            };

            rec.Id = this.repository.InsertClient(rec);
            return rec;
        }
    }
}
