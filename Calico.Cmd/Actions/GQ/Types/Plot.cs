// <copyright file="Plot.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd.Actions.GQ.Types
{
    using GraphQL.Types;

    public class Plot : ObjectGraphType<PlotRecord>
    {
        public Plot(IRepository repository)
        {
            this.Field(x => x.Id);
            this.Field(x => x.Name);
            this.Field(x => x.Wkt);
        }
    }
}
