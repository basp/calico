namespace Calico
{
    public static class DefaultArgDescriptions
    {
        public const string ClientId = @"The id of the client that owns the data";
        public const string PlotId = @"The id of the plot that the data is associated with";
        public const string FeatureTypeId = @"The id of the feature type that describes the meta data";
        public const string Shapefile = @"The shapefile to pull data from";
        public const string TargetDataSet = @"The id of the data set that is going to be refreshed";
        public const string Top = @"Max number of records to fetch";
        public const string SpatialReferenceSystem = @"The spatial reference system id (SRID) to use";
    }
}
