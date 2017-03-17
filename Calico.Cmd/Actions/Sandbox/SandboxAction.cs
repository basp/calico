// <copyright file="SandboxAction.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico.Cmd
{
    using System;
    using System.Linq;
    using Calico.Data;

    public class SandboxAction : IAction<SandboxArgs>
    {
        private CalicoContext context;

        public SandboxAction(CalicoContext context)
        {
            this.context = context;
        }

        public void Execute(SandboxArgs args)
        {
            var features = this.context.Features.ToList();

            foreach (var f in features)
            {
                Console.WriteLine(f.Geometry.AsText());
            }

            Console.WriteLine("Ok");
        }
    }
}
