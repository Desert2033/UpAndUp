using System;

public class BootstrapState : IState
{
    private const string InitialScene = "Initial";
    private GameStateMachine _stateMachine;
    private SceneLoader _sceneLoader;
    private AllServices _services;

    public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
    {
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;
        _services = services;

        RegisterServices();
    }

    public void Enter()
    {
        _sceneLoader.Load(InitialScene, onLoaded: EnterLoadLevel);
    }

    private void EnterLoadLevel() =>
        _stateMachine.Enter<LoadProgressState>();

    private void RegisterServices()
    {
        RegisterStaticDataService();

        _services.RegisterSingle<IAssets>(new AssetProvider());
        _services.RegisterSingle<IInputService>(new InputService());
        _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
        _services.RegisterSingle<IGameFactory>(new GameFactory(
            _services.Single<IAssets>(), 
            _services.Single<IStaticDataService>()));
        _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistentProgressService>(), _services.Single<IGameFactory>()));
        
    }

    private void RegisterStaticDataService()
    {
        StaticDataService staticDataService = new StaticDataService();

        staticDataService.LoadHeroLevels();
        staticDataService.LoadEnemyRangeLevels();
        staticDataService.LoadSpawnData();

        _services.RegisterSingle<IStaticDataService>(staticDataService);
    }

    public void Exit()
    {
    }
}
