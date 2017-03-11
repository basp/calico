// <copyright file="NewStyleCommand.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using Optional;

    using static Optional.Option;

    using Req = NewStyleRequest;
    using Res = NewStyleResponse;

    public class NewStyleCommand : ICommand<Req, Res, Exception>
    {
        private readonly IRepository repository;

        public NewStyleCommand(IRepository repository)
        {
            this.repository = repository;
        }

        public Option<Res, Exception> Execute(Req req)
        {
            try
            {
                var style = this.InsertStyle(req);
                var res = new Res { Style = style };
                return Some<Res, Exception>(res);
            }
            catch (Exception ex)
            {
                return None<Res, Exception>(ex);
            }
        }

        private StyleRecord InsertStyle(Req req)
        {
            var rec = new StyleRecord
            {
                FeatureTypeId = req.FeatureTypeId,
                AttributeIndex = req.AttributeIndex,
                StyleTypeId = req.StyleTypeId,
                Name = req.Name,
            };

            rec.Id = this.repository.InsertStyle(rec);
            return rec;
        }
   }
}
