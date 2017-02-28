// <copyright file="FeatureTypeRecord.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    public class FeatureTypeRecord
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public string Name { get; set; }

        public byte[] Checksum { get; set; }
    }
}
