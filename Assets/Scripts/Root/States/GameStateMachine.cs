using System;
using System.Collections.Generic;

public class GameStateMachine
{
    private Dictionary<Type, IExitableState> _states;
    private IExitableState _currentState;

    public GameStateMachine(SceneLoader sceneLoader, AllServices services)
    {
        _states = new Dictionary<Type, IExitableState>()
        {
            [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
            
            [typeof(LoadProgressState)] = new LoadProgressState(this, 
            services.Single<IPersistentProgressService>(), 
            services.Single<ISaveLoadService>()),
            
            [typeof(LoadLevelState)] = new LoadLevelState(this, 
            sceneLoader, 
            services.Single<IGameFactory>(), 
            services.Single<IPersistentProgressService>(), 
            services.Single<IInputService>(),
            services.Single<IStaticDataService>()),
            
            [typeof(GameLoopState)] = new GameLoopState(this, 
            services.Single<IGameFactory>(), 
            services.Single<ISaveLoadService>()),
        };
    }

    public void Enter<TState>() where TState : class, IState
    {
        IState state = ChangeState<TState>();
        state.Enter();
    }

    public void Enter<TState, TParameter>(TParameter parameter) where TState : class, IStateWithParameter<TParameter>
    {
        TState state = ChangeState<TState>();
        state.Enter(parameter);
    }

    private TState ChangeState<TState>() where TState : class, IExitableState
    {
        _currentState?.Exit();

        TState state = GetState<TState>();
        _currentState = state;
        
        return state;
    }

    private TState GetState<TState>() where TState : class, IExitableState =>
        _states[typeof(TState)] as TState;
}
