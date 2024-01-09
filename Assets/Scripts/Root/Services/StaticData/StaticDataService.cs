using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StaticDataService : IStaticDataService
{
    private List<HeroLevelData> _heroLevelsData;
    private List<EnemyRangeData> _enemyRangeLevelsData;
    private List<SpawnerData> _spawnerData;

    public void LoadEnemyRangeLevels()
    {
        _enemyRangeLevelsData =
            Resources.LoadAll<EnemyRangeData>("StaticData/EnemyRangeLevels")
            .OrderBy(x => x.Level)
            .ToList();
    }

    public void LoadHeroLevels()
    {
        _heroLevelsData =
            Resources.LoadAll<HeroLevelData>("StaticData/HeroLevels")
            .OrderBy(x => x.Level)
            .ToList();
    }

    public void LoadSpawnData() => 
        _spawnerData = Resources.LoadAll<SpawnerData>("StaticData/Spawners").ToList();

    public HeroLevelData ForHeroLevel(int level) =>
        _heroLevelsData[level - 1];

    public EnemyRangeData ForEnemyRangeByCountBlocks(uint countBlocks) =>
        _enemyRangeLevelsData.Find(data => data.MaxBlocks > countBlocks && data.MinBlocks <= countBlocks);

    public SpawnerData ForSpawnDataByCountBlocks(uint countBlocks) =>
        _spawnerData.Find(data => data.MaxBlocks > countBlocks && data.MinBlocks <= countBlocks);
}
