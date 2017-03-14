// <copyright file="GetGeoJsonCommand.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using System;
    using Optional;

    using static Optional.Option;

    using Req = GetGeoJsonRequest;
    using Res = GetGeoJsonResponse;

    public class GetGeoJsonCommand : ICommand<Req, Res, Exception>
    {
        public Option<Res, Exception> Execute(Req req)
        {
            try
            {
                return None<Res, Exception>(new NotImplementedException());
            }
            catch (Exception ex)
            {
                return None<Res, Exception>(ex);
            }
        }
    }
}
