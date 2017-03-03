// <copyright file="IAction.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    public interface IAction<TArgs>
    {
        void Execute(TArgs args);
    }
}
