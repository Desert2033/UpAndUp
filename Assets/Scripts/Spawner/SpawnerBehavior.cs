using UnityEngine;

public class SpawnerBehavior : MonoBehaviour, IReactionOfHeroDeath
{
    [SerializeField] private SpawnerGrid _spawnerGrid;

    private IStaticDataService _staticDataService;
    private Spawner _spawner;
    private Timer _cooldown;
    private SpawnerData _currentSpawnerData;
    private CounterBlocks _counterBlocks;
    private int MaxBlocks;

    public void Construct(Transform heroTransform,
        IGameFactory gameFactory,
        IStaticDataService staticDataService,
        CounterBlocks counterBlocks)
    {
        _staticDataService = staticDataService;
        _counterBlocks = counterBlocks;
        HeroHealth heroHealth = heroTransform.GetComponent<HeroHealth>();
        CameraBorder cameraBorder = Camera.main.GetComponent<CameraBorder>();

        _spawner = new Spawner(gameFactory, 
            heroHealth, 
            this.transform, 
            cameraBorder, 
            counterBlocks,
            _spawnerGrid);
    }

    private void Start()
    {
        _currentSpawnerData = _staticDataService.ForSpawnDataByCountBlocks(0);
        _cooldown = new Timer(_currentSpawnerData.Duration);
    }

    private void Update()
    {
        ChangeSpawnerData();

        Spawn();

        _cooldown.Tick(Time.deltaTime);
    }

    public void OnHeroDie()
    {
        this.enabled = false;
    }

    private void ChangeSpawnerData()
    {
        if (_counterBlocks.CountBlocks >= MaxBlocks)
        {
            _currentSpawnerData = _staticDataService.ForSpawnDataByCountBlocks(_counterBlocks.CountBlocks);
            _cooldown.ChangeDuretion(_currentSpawnerData.Duration);
        }
    }

    private void Spawn()
    {
        if (_cooldown.CurrentDuretion <= 0)
        {
            _spawner.Spawn(_currentSpawnerData);
            _cooldown.Restart();
        }
    }
}
