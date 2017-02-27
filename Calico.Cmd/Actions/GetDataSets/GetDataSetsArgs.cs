﻿namespace Calico.Cmd
{
    using PowerArgs;

    public class GetDataSetsArgs
    {
        [ArgRequired]
        [ArgDescription(DefaultArgDescriptions.PlotId)]
        public int PlotId { get; set; }

        [ArgDefaultValue(50)]
        [ArgDescription(DefaultArgDescriptions.Top)]
        public int Top { get; set; }
    }
}
