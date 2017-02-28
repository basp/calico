// <copyright file="ICommand.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    using Optional;

    public interface ICommand<TReq, TRes, TEx>
    {
        Option<TRes, TEx> Execute(TReq req);
    }
}
