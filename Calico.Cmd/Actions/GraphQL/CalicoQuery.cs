// <copyright file="CalicoQuery.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd.Actions.GraphQL
{
    using global::GraphQL.Types;
    using Types;

    public class CalicoQuery : ObjectGraphType
    {
        public CalicoQuery(IRepository repository)
        {
            this.Field<Client>(
                name: "client",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>>()
                    {
                        Name = "id",
                    }),
                resolve: c => repository.GetClient(c.GetArgument<int>("id")));
        }
    }
}
