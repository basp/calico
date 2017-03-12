// <copyright file="DataTypeModel.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Web.Models
{
    public class DataTypeModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string SqlType { get; set; }

        public string BclType { get; set; }
    }
}
