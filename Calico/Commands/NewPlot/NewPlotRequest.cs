﻿namespace Calico
{
    public class NewPlotRequest
    {
        public int ClientId { get; set; }

        public string PathToShapefile { get; set; }

        public string Name { get; set; }

        public int SRID { get; set; }
    }
}
