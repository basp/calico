namespace Calico.Cmd
{
    public interface IAction<TArgs>
    {
        void Execute(TArgs args);
    }
}
