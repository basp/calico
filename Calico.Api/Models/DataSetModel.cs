namespace Calico.Api.Models
{
    using System;

    public class DataSetModel
    {
        public int Id { get; set; }

        public int PlotId { get; set; }

        public string Name { get; set; }

        public DateTime DateCreated { get; set; }
    }
}