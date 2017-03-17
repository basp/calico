namespace Calico.Graph
{
    using System;
    using GraphQL.Types;

    public class CalicoSchema : Schema
    {
        public CalicoSchema(Func<Type,GraphType> resolveType)
            : base(resolveType)
        {
            this.Query = (CalicoQuery)resolveType(typeof(CalicoQuery));
        }
    }
}