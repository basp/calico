﻿// <copyright file="GetCategoriesArgs.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using PowerArgs;

    public class GetCategoriesArgs
    {
        [ArgRequired]
        [ArgPosition(1)]
        public string PathToShapefile { get; set; }

        [ArgRequired]
        [ArgPosition(2)]
        public string ColumnName { get; set; }
    }
}