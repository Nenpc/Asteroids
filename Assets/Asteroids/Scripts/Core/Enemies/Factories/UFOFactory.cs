using Asteroids.Scripts.Core.Base;
using Asteroids.Scripts.Core.Enemies.Config;
using Asteroids.Scripts.Core.Enemies.Views;
using Asteroids.Scripts.Core.Hero.Models;
using Asteroids.Scripts.Core.Progress.Interfeces;
using Asteroids.Scripts.Infrastructure.Pools;
using Asteroids.Scripts.Infrastructure.UpdateProvider;
using UnityEngine;

namespace Asteroids.Scripts.Core.Enemies.Models
{
    public sealed class UFOFactory : EnemyFactoryAbstract<UFOEnemy>
    {
        public override Enums.Enemies EnemyType => Enums.Enemies.UFO;

        private readonly IHeroModel _heroModel;
        private EnemyConfig _UFOConfig;
        
        public UFOFactory(IHeroModel heroModel, 
            IEnemiesConfig enemiesConfig, 
            IUpdateProvider updateProvider, 
            IProgressModel progressModel) : 
            base(enemiesConfig, updateProvider, progressModel)
        {
            _heroModel = heroModel;
            _UFOConfig = enemiesConfig.GetEnemyView(Enums.Enemies.UFO);
            _poolViews = new PoolGameObject(_UFOConfig.View.gameObject, _UFOConfig.MaxCount);
            _spawnDelay = _UFOConfig.SpawnDelaySecond;
            _pastTime = 0;
        }

        public override void Start()
        {
            for (int i = 0; i < _UFOConfig.StartAmount; i++)
            {
                CreateEnemy();
            }
            _pastTime = 0;
        }

        protected override void CheckLifetime()
        {

        }

        public override UFOEnemy CreateEnemy()
        {
            var enemyView = _poolViews.Get().GetComponent<EnemyView>();
            var model = new UFOEnemy(_heroModel, _UFOConfig.Speed, enemyView, _updateProvider, _progressModel);
            enemyView.Init(model);
            model.OnDestroy += ReturnToPool;
            _activeEnemies.Add(model);
            
            var startBorder = Random.Range(0, 4);
            var startPosition = GetStartPosition(startBorder);

            enemyView.transform.position = startPosition;
            enemyView.transform.rotation = Quaternion.identity;
            
            enemyView.gameObject.SetActive(true);

            return null;
        }
    }
}