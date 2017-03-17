// <copyright file="CalicoSchema.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd.Actions.GraphQL
{
    using System;
    using global::GraphQL.Types;

    public class CalicoSchema : Schema
    {
        public CalicoSchema(Func<Type, GraphType> resolveType)
            : base(resolveType)
        {
            this.Query = (CalicoQuery)resolveType(typeof(CalicoQuery));
        }
    }
}
