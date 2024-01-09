public interface IStaticDataService : IService
{
    public EnemyRangeData ForEnemyRangeByCountBlocks(uint countBlocks);
    public HeroLevelData ForHeroLevel(int level);
    public SpawnerData ForSpawnDataByCountBlocks(uint countBlocks);
    public void LoadEnemyRangeLevels();
    public void LoadHeroLevels();
    public void LoadSpawnData();
}