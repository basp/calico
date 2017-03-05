// <copyright file="ImportShapefileCommand.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using Optional;

    using static Optional.Option;

    using Req = ImportShapefileRequest;
    using Res = ImportShapefileResponse;

    public class ImportShapefileCommand : ICommand<Req, Res, Exception>
    {
        public Option<Res, Exception> Execute(Req req)
        {
            throw new NotImplementedException();
        }
    }
}
