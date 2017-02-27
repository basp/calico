namespace Calico
{
    using Optional;

    public interface ICommand<TReq, TRes, TEx>
    {
        Option<TRes, TEx> Execute(TReq req);
    }
}
