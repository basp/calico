// <copyright file="ImportPlotResponse.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    public class ImportPlotResponse
    {
        public PlotRecord Plot { get; set; }

        public DataSetRecord DataSet { get; set; }

        public int NumberOfFeatures { get; set; }

        public int NumberOfAttributes { get; set; }
    }
}
