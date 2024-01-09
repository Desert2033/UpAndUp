using System.Collections.Generic;
using UnityEngine;

public interface IGameFactory : IService
{
    public List<ISavedProgressReader> ProgressReaders { get; }
    public List<ISavedProgress> ProgressWriters { get; }
    public List<IReactionOfHeroDeath> ReactionsOfHeroDeath { get; }
    public GameObject Hero { get; }

    public void Cleanup();
    public void RemoveFromReactionOfHeroDeath(IReactionOfHeroDeath gameObject);
    public GameObject CreateHero(GameObject at, Quaternion rotation, int level);
    public GameObject CreateBlockEarth(Vector3 at);
    public GameObject CreateTNT(Vector3 at);
    public GameObject CreateEnemyRange(Vector3 at, HeroHealth heroHealth, CameraBorder cameraBorder, CounterBlocks counterBlocks);
    public GameObject CreateBullet(Vector3 at, Vector3 direction, float damage, string pathBulletPrefab, Transform targetTransform);
    public GameObject CreateSpawner(Vector3 at);
    public GameObject CreateHud();
    public GameObject CreateRestartMenu();
    public GameObject CreateTNTMoveEasy(Vector3 at);
    public GameObject CreateTNTMoveHard(Vector3 at);
    public GameObject CreateTNTMoveDown(Vector3 at, CameraBorder cameraBorder);
    public GameObject CreateExperience(Vector3 at);
    public GameObject CreateHeal(Vector3 at);
}