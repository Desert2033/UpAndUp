public interface IStateWithParameter<TParameter> : IExitableState
{
    public void Enter(TParameter parameter);
}
