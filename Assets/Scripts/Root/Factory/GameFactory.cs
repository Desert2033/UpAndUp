﻿using System;
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

    public GameObject CreateExperience(Vector3 at) =>
        _assets.Instantiate(AssetPath.PathExperience, at, Quaternion.Euler(-20, 90, 0));

    public GameObject CreateHeal(Vector3 at) =>
        _assets.Instantiate(AssetPath.PathHeal, at);

    public GameObject CreateTNT(Vector3 at) =>
        _assets.Instantiate(AssetPath.PathTNT, at);

    public GameObject CreateTNTMoveEasy(Vector3 at) => 
        _assets.Instantiate(AssetPath.PathTNTMoveEasy, at);

    public GameObject CreateTNTMoveHard(Vector3 at) => 
        _assets.Instantiate(AssetPath.PathTNTMoveHard, at);

    public GameObject CreateTNTMoveDown(Vector3 at, CameraBorder cameraBorder)
    {
        GameObject tnt = _assets.Instantiate(AssetPath.PathTNTMoveDown, at);
        tnt.GetComponentInChildren<TNTDown>().Construct(cameraBorder);

        return tnt;
    }

    public GameObject CreateSpawner(Vector3 at) => 
        InstantiateRegistered(AssetPath.PathSpawner, at, Quaternion.identity);

    public GameObject CreateRestartMenu() => 
        _assets.Instantiate(AssetPath.PathRestartMenu);

    public GameObject CreateHud() => 
        InstantiateRegistered(AssetPath.PathHud);

    public GameObject CreateEnemyRange(Vector3 at, HeroHealth heroHealth, CameraBorder cameraBorder, CounterBlocks counterBlocks)
    {
        GameObject enemyRange = InstantiateRegistered(AssetPath.PathRangeEnemy, at, Quaternion.identity);
        EnemyRangeData enemyData = _staticData.ForEnemyRangeByCountBlocks(30);

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
        if(ReactionsOfHeroDeath.Contains(gameObject))
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
