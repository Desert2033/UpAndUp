using UnityEngine;

public class SpawnerBehavior : MonoBehaviour, IReactionOfHeroDeath
{
    [SerializeField] private SpawnerGrid _spawnerGrid;

    private SpawnerEasy _spawnerEasy;

    public void Construct(Transform heroTransform, IGameFactory gameFactory, CounterBlocks counterBlocks)
    {
        HeroHealth heroHealth = heroTransform.GetComponent<HeroHealth>();
        CameraBorder cameraBorder = Camera.main.GetComponent<CameraBorder>();

        _spawnerEasy = new SpawnerEasy(gameFactory, 
            heroHealth, 
            this.transform, 
            cameraBorder, 
            _spawnerGrid,
            counterBlocks);
    }

    private void Update()
    {
        _spawnerEasy.Spawn(Time.deltaTime);
    }

    public void OnHeroDie()
    {
        this.enabled = false;
    }
}
