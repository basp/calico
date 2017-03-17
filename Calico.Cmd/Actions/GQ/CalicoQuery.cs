// <copyright file="CalicoQuery.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd.Actions.GQ
{
    using GraphQL.Types;
    using Types;

    public class CalicoQuery : ObjectGraphType
    {
        private const int ClientId = 1;

        public CalicoQuery(IRepository repository)
        {
            this.Field<Client>(
                name: "client",
                resolve: c => repository.GetClient(ClientId));
        }
    }
}
