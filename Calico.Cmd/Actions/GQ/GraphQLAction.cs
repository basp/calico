// <copyright file="GraphQLAction.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using System;
    using System.Data.SqlClient;
    using Actions.GQ;
    using GraphQL;
    using GraphQL.Http;
    using GraphQL.Types;
    using SimpleInjector;

    public class GraphQLAction : IAction<GraphQLArgs>
    {
        private readonly Func<SqlConnection> connectionFactory;
        private readonly Container container;

        public GraphQLAction(Func<SqlConnection> connectionFactory)
        {
            this.connectionFactory = connectionFactory;
            this.container = new Container();
        }

        public async void Execute(GraphQLArgs args)
        {
            using (var conn = this.connectionFactory())
            using (var session = SqlSession.Open(conn))
            {
                var repo = new SqlRepository(session);

                this.container.Register<IRepository>(() => repo);
                this.container.Register<CalicoQuery>();
                this.container.Register<Actions.GQ.Types.Client>();
                this.container.Register<Actions.GQ.Types.Plot>();

                var schema = new CalicoSchema(
                    x => (GraphType)this.container.GetInstance(x));

                var q = args.Query;
                var result = await new DocumentExecuter().ExecuteAsync(x =>
                {
                    x.Schema = schema;
                    x.Query = q;
                }).ConfigureAwait(false);

                var json = new DocumentWriter(indent: true).Write(result);
                Console.WriteLine(json);
            }
        }
    }
}
