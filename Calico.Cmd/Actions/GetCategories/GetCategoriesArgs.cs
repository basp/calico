// <copyright file="GetCategoriesArgs.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using PowerArgs;

    public class GetCategoriesArgs
    {
        [ArgRequired]
        public string PathToShapefile { get; set; }

        [ArgRequired]
        public string ColumnName { get; set; }
    }
}
