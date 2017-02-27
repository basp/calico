namespace Calico
{
    using System;
    using Optional;

    using static Optional.Option;

    using Req = ImportFeaturesRequest;
    using Res = ImportFeaturesResponse;

    public class ImportFeaturesCommand : ICommand<Req, Res, Exception>
    {
        private readonly IRepository repository;

        public ImportFeaturesCommand(IRepository repository)
        {
            this.repository = repository;
        }

        public Option<Res, Exception> Execute(Req req)
        {
            throw new NotImplementedException();
        }
    }
}
