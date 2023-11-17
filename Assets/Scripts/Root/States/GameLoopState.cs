using UnityEngine;
using UnityEngine.UI;

public class GameLoopState : IState
{
    private GameStateMachine _stateMachine;
    private IGameFactory _gameFactory;
    private ISaveLoadService _progressService;
    private HeroDeath _heroDeath;

    public GameLoopState(GameStateMachine gameStateMachine, IGameFactory gameFactory, ISaveLoadService progressService)
    {
        _stateMachine = gameStateMachine;
        _gameFactory = gameFactory;
        _progressService = progressService;
    }

    public void Enter()
    {
        _heroDeath = _gameFactory.Hero.GetComponent<HeroDeath>();
        _heroDeath.OnDie += OnHeroDie;
    }

    public void Exit()
    {
        _heroDeath.OnDie -= OnHeroDie;
    }

    private void OnHeroDie()
    {
        CheckReactionOfHeroDeath();

        _progressService.SavedProgress();

        ShowRestartMenu();
    }

    private void CheckReactionOfHeroDeath()
    {
        foreach (IReactionOfHeroDeath reactionOfHeroDeath in _gameFactory.ReactionsOfHeroDeath)
        {
            reactionOfHeroDeath.OnHeroDie();
        }
    }
    private void ShowRestartMenu()
    {
        Button buttonRestart = _gameFactory.CreateRestartMenu().transform.GetChild(1).GetComponent<Button>();
        buttonRestart.onClick.AddListener(RestartGame);
    }

    private void RestartGame() => 
        _stateMachine.Enter<LoadProgressState>();
}