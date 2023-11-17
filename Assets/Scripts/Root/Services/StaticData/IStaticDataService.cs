public interface IStaticDataService : IService
{
    public EnemyRangeData ForEnemyRangeByCountBlocks(uint countBlocks);
    public HeroLevelData ForHeroLevel(int level);
    public void LoadEnemyRangeLevels();
    public void LoadHeroLevels();
}