using UnityEngine;

public class LoadProgressState : IState
{
    private GameStateMachine _gameStateMachine;
    private IPersistentProgressService _progressService;
    private ISaveLoadService _saveLoadService;

    public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoadService)
    {
        _gameStateMachine = gameStateMachine;
        _progressService = progressService;
        _saveLoadService = saveLoadService;
    }

    public void Enter()
    {
        LoadProgressStateOrInitNew();
        _gameStateMachine.Enter<LoadLevelState>();
    }

    public void Exit()
    {
    }

    private void LoadProgressStateOrInitNew() => 
        _progressService.Progress = _saveLoadService.LoadProgress() ?? NewProgress();

    private PlayerProgress NewProgress() => 
        new PlayerProgress();
}