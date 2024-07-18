using Asteroids.Scripts.Core.Enemies.Config;

namespace Asteroids.Scripts.Core.Enemies.Models
{
    public sealed class EnemiesCreator : IEnemiesCreator
    {
        private IEnemiesConfig _enemyConfig;
        
        public EnemiesCreator(IEnemiesConfig enemyConfig)
        {
            _enemyConfig = enemyConfig;
        }

        public void Start()
        {
            throw new System.NotImplementedException();
        }

        public void FixedUpdate()
        {
            throw new System.NotImplementedException();
        }

        public void End()
        {
            throw new System.NotImplementedException();
        }
    }
}