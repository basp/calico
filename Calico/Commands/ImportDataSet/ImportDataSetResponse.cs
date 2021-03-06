﻿// <copyright file="ImportDataSetResponse.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    public class ImportDataSetResponse
    {
        public PlotRecord Plot { get; set; }

        public DataSetRecord DataSet { get; set; }

        public int NumberOfFeatures { get; set; }

        public int NumberOfAttributes { get; set; }
    }
}
