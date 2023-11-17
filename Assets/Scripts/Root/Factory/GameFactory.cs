using System;
using System.Collections.Generic;
using UnityEngine;

public class GameFactory : IGameFactory
{
    private IAssets _assets;
    private IStaticDataService _staticData;

    public GameObject Hero { get; private set; }

    public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
    public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
    public List<IReactionOfHeroDeath> ReactionsOfHeroDeath { get; } = new List<IReactionOfHeroDeath>();

    public GameFactory(IAssets assets, IStaticDataService staticData)
    {
        _assets = assets;
        _staticData = staticData;
    }

    public GameObject CreateHero(GameObject at, Quaternion rotation, int level)
    {
        HeroLevelData staticData = _staticData.ForHeroLevel(level);
        Hero = InstantiateRegistered(AssetPath.PathPlayer, at.transform.position, rotation);

        HeroHealth heroHealth = Hero.GetComponent<HeroHealth>();
        heroHealth.SetMaxHp(staticData.Hp);

        HeroAttack heroAttack = Hero.GetComponent<HeroAttack>();
        heroAttack.SetDamage(staticData.Damage);

        return Hero;
    }

    public GameObject CreateBlockEarth(Vector3 at) =>
        _assets.Instantiate(AssetPath.PathBlockEarth, at);

    public GameObject CreateTNT(Vector3 at) =>
        _assets.Instantiate(AssetPath.PathTNT, at);

    public GameObject CreateSpawner(Vector3 at) => 
        InstantiateRegistered(AssetPath.PathSpawner, at, Quaternion.identity);

    public GameObject CreateRestartMenu() => 
        _assets.Instantiate(AssetPath.PathRestartMenu);

    public GameObject CreateHud() => 
        InstantiateRegistered(AssetPath.PathHud);

    public GameObject CreateEnemyRange(Vector3 at, HeroHealth heroHealth, CameraBorder cameraBorder, CounterBlocks counterBlocks)
    {
        Vector3 fixRotation = new Vector3(0f, 180f, 0f);
        GameObject enemyRange = InstantiateRegistered(AssetPath.PathRangeEnemy, at, Quaternion.Euler(fixRotation));
        EnemyRangeData enemyData = _staticData.ForEnemyRangeByCountBlocks(counterBlocks.CountBlocks);

        enemyRange.GetComponent<EnemyRangeObserver>().Construct(heroHealth.transform, cameraBorder);
        enemyRange.GetComponent<EnemyRangeAttack>().Construct(heroHealth, cameraBorder, this, enemyData.Damage);
        enemyRange.GetComponent<EnemyHealth>().Construct(enemyData.Health);
        enemyRange.GetComponent<EnemyDeath>().Construct(this);
        
        return enemyRange;
    }

    public GameObject CreateBullet(Vector3 at, Vector3 direction, float damage, string pathBulletPrefab, Transform targetTransform)
    {
        GameObject bullet = _assets.Instantiate(pathBulletPrefab, at);

        bullet.GetComponent<BulletMove>().Construct(direction, targetTransform);
        bullet.GetComponent<BulletAttack>().Construct(damage, targetTransform, this);

        return bullet;
    }

    public void RemoveFromReactionOfHeroDeath(IReactionOfHeroDeath gameObject)
    {
        ReactionsOfHeroDeath.Remove(gameObject);
    }

    public void Cleanup()
    {
        ProgressReaders.Clear();
        ProgressWriters.Clear();
        ReactionsOfHeroDeath.Clear();
    }

    private GameObject InstantiateRegistered(string prefabPath)
    {
        GameObject gameObject = _assets.Instantiate(prefabPath);
        RegisterProgressWatchers(gameObject);
        RegisterReactionsOfHeroDeath(gameObject);
        return gameObject;
    }

    private GameObject InstantiateRegistered(string prefabPath, Vector3 at, Quaternion rotation)
    {
        GameObject gameObject = _assets.Instantiate(prefabPath, at, rotation);
        RegisterProgressWatchers(gameObject);
        RegisterReactionsOfHeroDeath(gameObject);
        return gameObject;
    }

    private void RegisterReactionsOfHeroDeath(GameObject gameObject)
    {
        foreach (IReactionOfHeroDeath reactionDeath in gameObject.GetComponentsInChildren<IReactionOfHeroDeath>())
        {
            ReactionsOfHeroDeath.Add(reactionDeath);
        }
    }

    private void RegisterProgressWatchers(GameObject gameObject)
    {
        foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
        {
            Register(progressReader);
        }
    }

    private void Register(ISavedProgressReader progressReader)
    {
        if (progressReader is ISavedProgress progressWriter)
            ProgressWriters.Add(progressWriter);

        ProgressReaders.Add(progressReader);
    }
}
