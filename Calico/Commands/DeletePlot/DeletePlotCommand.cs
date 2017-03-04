// <copyright file="DeletePlotCommand.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using Optional;
    using Serilog;

    using static Optional.Option;

    using Req = DeletePlotRequest;
    using Res = DeletePlotResponse;

    public class DeletePlotCommand : ICommand<Req, Res, Exception>
    {
        private readonly IRepository repository;

        public DeletePlotCommand(IRepository repository)
        {
            this.repository = repository;
        }

        public Option<Res, Exception> Execute(Req req)
        {
            try
            {
                var plot = this.repository.GetPlot(req.Id);
                Log.Information(
                    "Deleting plot {PlotName} with id {PlotId}",
                    plot.Name,
                    plot.Id);

                var modified = this.repository.DeletePlot(req.Id);
                var res = new Res
                {
                    Plot = plot,
                    RowCount = modified,
                };

                return Some<Res, Exception>(res);
            }
            catch (Exception ex)
            {
                return None<Res, Exception>(ex);
            }
        }
    }
}
