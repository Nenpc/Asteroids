namespace Asteroids.Scripts.Core.Enemies.Config
{
    public interface IEnemiesConfig
    {
        EnemyConfig GetEnemyView(Enums.Enemies enemyType);
    }
}