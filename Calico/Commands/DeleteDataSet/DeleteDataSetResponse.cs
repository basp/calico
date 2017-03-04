// <copyright file="DeleteDataSetResponse.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    public class DeleteDataSetResponse
    {
        public DataSetRecord DataSet { get; set; }

        public int RowCount { get; set; }
    }
}
