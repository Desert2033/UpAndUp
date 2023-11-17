using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerEasy
{
    private Timer _cooldown;
    private float _minDuration;
    private float _maxDuration;
    private IGameFactory _gameFactory;
    private Transform _spawnBehaviour;
    private GridMaker _gridMaker;
    private HeroHealth _heroHealth;
    private CameraBorder _cameraBorder;
    private SpawnerGrid _spawnerGrid;
    private CounterBlocks _counterBlocks;

    public SpawnerEasy(IGameFactory gameFactory,
        HeroHealth heroHealth,
        Transform spawnBehavior,
        CameraBorder cameraBorder,
        SpawnerGrid spawnerGrid,
        CounterBlocks counterBlocks)
    {
        _gameFactory = gameFactory;
        _heroHealth = heroHealth;
        _spawnBehaviour = spawnBehavior;
        _cameraBorder = cameraBorder;
        _spawnerGrid = spawnerGrid;
        _counterBlocks = counterBlocks;

        _minDuration = 3f;
        _maxDuration = 7f;

        _gridMaker = new GridMaker();
        _cooldown = new Timer(_maxDuration);
    }

    public void Spawn(float step)
    {
        _cooldown.Tick(step);

        if (_cooldown.CurrentDuretion <= 0)
        {
            int random = Random.Range(1, 100);

            if (random <= 0)
                SpawnTNT();
            else
                SpawnEnemyRange();

            RestartCooldown();
        }
    }

    public void SpawnEnemyRange()
    {
        List<Vector3> line =
            _gridMaker.MakeLine(_spawnerGrid.GetStartPoint(), _spawnerGrid.GetEndPoint());

        int random = Random.Range(0, line.Count);

        _gameFactory.CreateEnemyRange(line[random], _heroHealth, _cameraBorder, _counterBlocks);
    }

    private void SpawnTNT()
    {
        Vector3 spawnPosition = _spawnBehaviour.position;
        spawnPosition.x = _heroHealth.transform.position.x;

        _gameFactory.CreateTNT(spawnPosition);
    }

    private void RestartCooldown()
    {
        float newDuretion = Random.Range(_minDuration, _maxDuration);

        _cooldown.ChangeDuretion(newDuretion);
        _cooldown.Restart();
    }
}
