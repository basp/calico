// <copyright file="GetDataSetCommand.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using System.Linq;
    using Optional;

    using static Optional.Option;

    using Req = GetDataSetRequest;
    using Res = GetDataSetResponse;

    public class GetDataSetCommand : ICommand<Req, Res, Exception>
    {
        private readonly IRepository repository;

        public GetDataSetCommand(IRepository repository)
        {
            this.repository = repository;
        }

        public Option<Res, Exception> Execute(Req req)
        {
            try
            {
                var dataSet = this.repository.GetDataSet(req.DataSetId);
                var featureType = this.repository.GetFeatureType(dataSet.FeatureTypeId);
                var features = this.repository.GetFeatures(req.DataSetId);
                var attributes = this.repository.GetAttributes(featureType.Id);
                var attributeValues = this.repository.GetAttributeValues(req.DataSetId);
                var dataTypes = this.repository.GetDataTypes();
                var res = new Res
                {
                    DataSet = dataSet,
                    FeatureType = featureType,
                    Attributes = attributes.ToList(),
                    Features = features.ToList(),
                    AttributeValues = attributeValues.ToList(),
                    DataTypes = dataTypes.ToList(),
                };

                return Some<Res, Exception>(res);
            }
            catch (Exception ex)
            {
                return None<Res, Exception>(ex);
            }
        }
    }
}
