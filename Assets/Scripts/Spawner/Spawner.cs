using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner
{
    private IGameFactory _gameFactory;
    private Transform _spawnBehaviour;
    private HeroHealth _heroHealth;
    private CameraBorder _cameraBorder;
    private CounterBlocks _counterBlocks;
    private SpawnerGrid _spawnerGrid;
    private GridMaker _gridMaker;
    private List<Vector3> _line;

    public Spawner(IGameFactory gameFactory,
        HeroHealth heroHealth,
        Transform spawnBehavior,
        CameraBorder cameraBorder,
        CounterBlocks counterBlocks,
        SpawnerGrid spawnerGrid)
    {
        _gameFactory = gameFactory;
        _heroHealth = heroHealth;
        _spawnBehaviour = spawnBehavior;
        _cameraBorder = cameraBorder;
        _counterBlocks = counterBlocks;
        _spawnerGrid = spawnerGrid;
        _gridMaker = new GridMaker();
    }

    public void SpawnEnemyRange()
    {
        Vector3 position = GetRandomPosition(_line.Count);

        const float OffsetZ = 5;
        const float OffsetYFromEnemy = 0.91f;

        position.z += OffsetZ;

        Vector3 blockPosition = position;
        blockPosition.y -= OffsetYFromEnemy;

        _gameFactory.CreateEnemyRange(position, _heroHealth, _cameraBorder, _counterBlocks);
        _gameFactory.CreateBlockEarth(blockPosition);
    }

    public void Spawn(SpawnerData currentSpawnerData)
    {
        _line = _gridMaker.MakeLine(_spawnerGrid.GetStartPoint(), _spawnerGrid.GetEndPoint());

        SpawnObstacle(currentSpawnerData.SpawnObstacles);
        SpawnBonus(currentSpawnerData.SpawnBonuses);
    }

    private void SpawnBonus(SpawnBonus[] spawnBonuses)
    {
        int indexChoose = ChooseBonus(spawnBonuses);

        if (indexChoose != -1) 
        {
            SpawnBonus bonus = spawnBonuses[indexChoose];

            switch (bonus.Type)
            {
                case TypeBonus.Experience:
                    SpawnExperience();
                    break;
                case TypeBonus.Health:
                    SpawnHeal();
                    break;
            }
        }
    }

    private void SpawnObstacle(SpawnObstacle[] obstacles)
    {
        int indexChoose = ChooseObstacle(obstacles);

        if (indexChoose != -1)
        {
            SpawnObstacle obstacle = obstacles[indexChoose];

            switch (obstacle.Type)
            {
                case TypeObstacles.TNT:
                    SpawnTNT();
                    break;
                case TypeObstacles.TNTMoveEasy:
                    SpawnTNTMoveEasy();
                    break;
                case TypeObstacles.TNTMoveHard:
                    SpawnTNTMoveHard();
                    break;
                case TypeObstacles.TNTMoveDown:
                    SpawnTNTMoveDown();
                    break;
                case TypeObstacles.TNTWall:
                    SpawnTNTWall();
                    break;
                case TypeObstacles.Skeleton:
                    SpawnEnemyRange();
                    break;
            }
        }
    }

    private int ChooseObstacle(SpawnObstacle[] obstacles)
    {
        float maxProcent = 100;
        float procent = maxProcent;
        float[] totalsMin = new float[obstacles.Length];
        float[] totalsMax = new float[obstacles.Length];
        int random = Random.Range(1, 100);

        for (int i = 0; i < obstacles.Length; i++)
        {
            totalsMax[i] = procent;
            procent -= obstacles[i].SpawnProcent;
            totalsMin[i] = procent;
        }

        for (int i = 0; i < obstacles.Length; i++)
        {
            if (obstacles[i].SpawnProcent == maxProcent)
                return i;
            else if (random >= totalsMin[i] && totalsMax[i] >= random)
                return i;
        }

        return -1;
    }

    private int ChooseBonus(SpawnBonus[] bonuses)
    {
        float maxProcent = 100;
        float procent = maxProcent;
        float[] totalsMin = new float[bonuses.Length];
        float[] totalsMax = new float[bonuses.Length];
        int random = Random.Range(1, 100);

        for (int i = 0; i < bonuses.Length; i++)
        {
            totalsMax[i] = procent;
            procent -= bonuses[i].SpawnProcent;
            totalsMin[i] = procent;
        }

        for (int i = 0; i < bonuses.Length; i++)
        {
            if (bonuses[i].SpawnProcent == maxProcent)
                return i;
            else if (random >= totalsMin[i] && totalsMax[i] >= random)
                return i;
        }

        return -1;
    }

    private void SpawnTNT()
    {
        Vector3 spawnPosition = _line[_line.Count / 2];
        _line.Remove(spawnPosition);

        _gameFactory.CreateTNT(spawnPosition);
    }

    private void SpawnTNTMoveDown()
    {
        Vector3 spawnPosition = _spawnBehaviour.position;
        spawnPosition.x = _heroHealth.transform.position.x;

        _gameFactory.CreateTNTMoveDown(spawnPosition, _cameraBorder);
    }

    private void SpawnTNTMoveEasy()
    {
        int indexMax = _line.Count / 3;

        Vector3 position = GetRandomPosition(indexMax);

        _gameFactory.CreateTNTMoveEasy(position);
    }

    private void SpawnTNTMoveHard()
    {
        _gameFactory.CreateTNTMoveHard(_spawnerGrid.GetStartPoint());
    }

    private void SpawnExperience()
    {
        Vector3 position = GetRandomPosition(_line.Count);

        _gameFactory.CreateExperience(position);
    }

    private void SpawnHeal()
    {
        Vector3 position = GetRandomPosition(_line.Count);

        _gameFactory.CreateHeal(position);
    }

    private void SpawnTNTWall()
    {
        int random = Random.Range(0, _line.Count);
        Vector3 empty = _line[random];

        foreach (Vector3 position in _line)
        {
            if (position != empty)
            {
                _gameFactory.CreateTNT(position);
                _line.Remove(position);
            }
        }
    }

    private Vector3 GetRandomPosition(int max)
    {
        int random = Random.Range(0, max);
        Vector3 position = _line[random];
        _line.Remove(_line[random]);

        return position;
    }
}
