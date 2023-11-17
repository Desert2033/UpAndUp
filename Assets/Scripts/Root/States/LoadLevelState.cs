using System;
using UnityEngine;

public class LoadLevelState : IState
{
    private const string PlayerSpawnPointTag = "PlayerSpawnPoint";
    private const string SceneName = "Main";
    private const string SpawnerPointTag = "SpawnerPoint";

    private GameStateMachine _stateMachine;
    private SceneLoader _sceneLoader;
    private IGameFactory _gameFactory;
    private IPersistentProgressService _progressService;
    private IInputService _inputService;
    private IStaticDataService _staticDataService;

    public LoadLevelState(GameStateMachine gameStateMachine, 
        SceneLoader sceneLoader, 
        IGameFactory gameFactory,
        IPersistentProgressService progressService,
        IInputService inputService,
        IStaticDataService staticDataService)
    {
        _stateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
        _gameFactory = gameFactory;
        _progressService = progressService;
        _inputService = inputService;
        _staticDataService = staticDataService;
    }

    public void Enter()
    {
        _gameFactory.Cleanup();
        _sceneLoader.Load(SceneName, onLoaded);
    }

    public void Exit()
    {
    }

    private void onLoaded()
    {
        InitGameWorld();
        InformProgressReaders();

        _stateMachine.Enter<GameLoopState>();
    }

    private void InformProgressReaders()
    {
        foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
        {
            progressReader.LoadProgess(_progressService.Progress);
        }
    }

    private void InitGameWorld()
    {
        const int HeroStartLevel = 1;

        GameObject playerSpawnPoint = GameObject.FindWithTag(PlayerSpawnPointTag);
        GameObject spawnerPoint = GameObject.FindWithTag(SpawnerPointTag);

        Camera camera = Camera.main;
        GameObject hero = InitHero(HeroStartLevel, playerSpawnPoint);
        GameObject hud = InitHud(hero);

        InitSpawner(spawnerPoint.transform.position, hero, camera, hud);

        camera.GetComponent<CameraFollow>().SetTarget(hero.transform);
    }

    private GameObject InitHero(int HeroStartLevel, GameObject playerSpawnPoint)
    {
        GameObject hero = _gameFactory.CreateHero(playerSpawnPoint, playerSpawnPoint.transform.rotation, HeroStartLevel);
        
        hero.GetComponent<HeroLevelChanger>().Construct(_staticDataService, HeroStartLevel);
        hero.GetComponent<HeroAttack>().Construct(_gameFactory, _inputService);
        
        return hero;
    }

    private void InitSpawner(Vector3 spawnerPosition, GameObject hero, Camera camera, GameObject hud)
    {
        GameObject spawner = _gameFactory.CreateSpawner(spawnerPosition);
        CounterBlocks counterBlocks = hud.GetComponent<CounterBlocks>();

        SpawnerBehavior spawnerBehavior = spawner.GetComponent<SpawnerBehavior>();
        spawnerBehavior.Construct(hero.transform, _gameFactory, counterBlocks);
    }

    private GameObject InitHud(GameObject hero)
    {
        GameObject hud = _gameFactory.CreateHud();
        CounterBlocks counterBlocks = hud.GetComponent<CounterBlocks>();
        InputUI inputUI = hud.GetComponent<InputUI>();
        HeroMovingBehaviour heroMoving = hero.GetComponent<HeroMovingBehaviour>();
        HeroLevelChanger heroLevelChanger = hero.GetComponent<HeroLevelChanger>();

        counterBlocks.Construct(heroMoving);

        inputUI.LevelUpButton.onClick.AddListener(heroLevelChanger.UpLevel);
        _inputService.SetInputUI(inputUI);

        return hud;
    }
}