// <copyright file="StyleClassRecord.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    public class StyleClassRecord
    {
        public int Id { get; set; }

        public int StyleId { get; set; }

        public string Symbol { get; set; }

        public string Legend { get; set; }

        public string Category { get; set; }

        public double? MinValue { get; set; }

        public double? MaxValue { get; set; }
    }
}
