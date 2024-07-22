namespace Asteroids.Scripts.Core.Enemies.Models
{
    public interface IEnemyFactory<T> 
        where T : BaseEnemy
    {
        Enums.Enemies EnemyType { get; }

        T CreateEnemy();

        void Start();

        void FixedUpdate();

        void End();
    }
}