namespace Calico
{
    using System;
    using Optional;

    using static Optional.Option;

    using Req = GetClientsRequest;
    using Res = GetClientsResponse;

    public class GetClientsCommand : ICommand<Req, Res, Exception>
    {
        public Option<Res, Exception> Execute(Req req)
        {
            throw new NotImplementedException();
        }
    }
}
