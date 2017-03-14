// <copyright file="IFeatureCollectionExtensions.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;

    public static class IFeatureCollectionExtensions
    {
        public static string ToGeoJson(this IFeatureCollection self)
        {
            throw new NotImplementedException();
        }

        private class FeatureModel
        {
            // GeoJson is lon, lat
        }

        private class FeatureCollectionModel
        {
        }
    }
}
