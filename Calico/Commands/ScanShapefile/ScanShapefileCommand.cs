namespace Calico
{
    using System;
    using Optional;

    using static Optional.Option;

    using Req = ScanShapefileRequest;
    using Res = ScanShapefileResponse;

    public class ScanShapefileCommand : ICommand<Req, Res, Exception>
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
