using UnityEngine;

public class SaveLoadService : ISaveLoadService
{
    private const string ProgressKey = "Progress";
    private IGameFactory _gameFactory;
    private IPersistentProgressService _progressService;

    public SaveLoadService(IPersistentProgressService progressService, IGameFactory gameFactory)
    {
        _gameFactory = gameFactory;
        _progressService = progressService;
    }

    public PlayerProgress LoadProgress() =>
        PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();

    public void SavedProgress()
    {
        foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriters)
        {
            progressWriter.UpdateProgress(_progressService.Progress);
        }

        PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
    }
}