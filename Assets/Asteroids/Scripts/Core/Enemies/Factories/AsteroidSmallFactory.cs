using Asteroids.Scripts.Core.Enemies.Config;
using Asteroids.Scripts.Core.Enemies.Views;
using Asteroids.Scripts.Core.Progress.Interfeces;
using Asteroids.Scripts.Infrastructure.Pools;
using Asteroids.Scripts.Infrastructure.UpdateProvider;
using UnityEngine;

namespace Asteroids.Scripts.Core.Enemies.Models
{
    public sealed class AsteroidSmallFactory : AsteroidBaseFactory<AsteroidSmallEnemy>, ICreateAsteroidPosition
    {
        public override Enums.Enemies EnemyType => Enums.Enemies.AsteroidSmall;
        private EnemyConfig _asteroidConfig;
        
        public AsteroidSmallFactory(IEnemiesConfig enemiesConfig, 
            IUpdateProvider updateProvider, 
            IProgressModel progressModel) : 
            base(enemiesConfig, updateProvider, progressModel)
        {
            _asteroidConfig = enemiesConfig.GetEnemyView(Enums.Enemies.AsteroidSmall);
            _poolViews = new PoolGameObject(_asteroidConfig.View.gameObject, _asteroidConfig.MaxCount);
            _spawnDelay = _asteroidConfig.SpawnDelaySecond;
            _pastTime = 0;
        }
        
        public override void Start()
        {
            for (int i = 0; i < _asteroidConfig.StartAmount; i++)
            {
                CreateEnemy();
            }
            _pastTime = 0;
        }
        
        protected override void CheckLifetime()
        {
            for (int i = 0; i < _activeEnemies.Count; i++)
            {
                if (_activeEnemies[i].Lifetime > _asteroidConfig.LifetimeSecond)
                {
                    _activeEnemies[i].Destroy();
                    i--;
                }
            }
        }
        
        public override AsteroidSmallEnemy CreateEnemy()
        {
            var enemyView = _poolViews.Get().GetComponent<EnemyView>();
            var model = new AsteroidSmallEnemy(enemyView, _asteroidConfig.Speed, _updateProvider, _progressModel);
            enemyView.Init(model);
            model.OnDestroy += ReturnToPool;
            _activeEnemies.Add(model);
            
            var startBorder = Random.Range(0, 4);
            var startPosition = GetStartPosition(startBorder);
            var startRotation = GetStartRotation(startBorder, startPosition);

            enemyView.transform.position = startPosition;
            enemyView.transform.rotation = Quaternion.Euler(0,0,startRotation);
            
            enemyView.gameObject.SetActive(true);

            return model;
        }
        
        public void CreateOnPosition(Transform transform, int amount)
        {
            for (int i = 0; i <amount; i++)
            {
                var enemyView = _poolViews.Get().GetComponent<EnemyView>();
                var model = new AsteroidSmallEnemy(enemyView, _asteroidConfig.Speed, _updateProvider, _progressModel);
                enemyView.Init(model);
                model.OnDestroy += ReturnToPool;
                _activeEnemies.Add(model);

                enemyView.transform.position = transform.position;
                enemyView.transform.rotation = Quaternion.Euler(0,0,Random.Range(0,360));
            
                enemyView.gameObject.SetActive(true);
            }
        }
    }
}