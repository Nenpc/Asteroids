using System;

namespace Asteroids.Scripts.Core.Enemies.Models
{
    public sealed class EnemiesCreator : IEnemiesCreator
    {
        private readonly IEnemyFactory<AsteroidSmallEnemy> _asteroidSmallFactory;
        private readonly IEnemyFactory<AsteroidBigEnemy> _asteroidBigFactory;
        private readonly IEnemyFactory<UFOEnemy> _UFOFactory;
        
        public EnemiesCreator(IEnemyFactory<AsteroidSmallEnemy> asteroidSmallFactory,
            IEnemyFactory<AsteroidBigEnemy> asteroidBigFactory,
            IEnemyFactory<UFOEnemy> UFOFactory)
        {
            _asteroidSmallFactory = asteroidSmallFactory;
            _asteroidBigFactory = asteroidBigFactory;
            _UFOFactory = UFOFactory;
        }

        public void Start()
        {
            _asteroidSmallFactory.Start();
            _asteroidBigFactory.Start();
            _UFOFactory.Start();
        }
        public void FixedUpdate()
        {
            _asteroidSmallFactory.FixedUpdate();
            _asteroidBigFactory.FixedUpdate();
            _UFOFactory.FixedUpdate();
        }

        public void End()
        {
            _asteroidSmallFactory.End();
            _asteroidBigFactory.End();
            _UFOFactory.End();
        }
    }
}