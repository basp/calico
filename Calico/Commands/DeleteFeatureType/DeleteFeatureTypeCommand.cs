// <copyright file="DeleteFeatureTypeCommand.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using Optional;

    using static Optional.Option;

    using Req = DeleteFeatureTypeRequest;
    using Res = DeleteFeatureTypeResponse;

    public class DeleteFeatureTypeCommand : ICommand<Req, Res, Exception>
    {
        private readonly IRepository repository;

        public DeleteFeatureTypeCommand(IRepository repository)
        {
            this.repository = repository;
        }

        public Option<Res, Exception> Execute(Req req)
        {
            try
            {
                var featureType = this.repository.GetFeatureType(req.Id);
                var modified = this.repository.DeleteFeatureType(req.Id);
                var res = new Res
                {
                    FeatureType = featureType,
                    RowCount = modified,
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
