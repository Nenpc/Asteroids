using System.Collections.Generic;
using Asteroids.Scripts.Core.Base;
using Asteroids.Scripts.Core.Enemies.Config;
using Asteroids.Scripts.Core.Progress.Interfeces;
using Asteroids.Scripts.Infrastructure.Pools;
using Asteroids.Scripts.Infrastructure.UpdateProvider;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids.Scripts.Core.Enemies.Models
{
    public abstract class EnemyFactoryAbstract<T> : IEnemyFactory<T> 
        where T : BaseEnemy
    {
        public abstract Enums.Enemies EnemyType { get; }
        
        protected readonly IEnemiesConfig _enemiesConfig;
        protected readonly IUpdateProvider _updateProvider;
        protected readonly IProgressModel _progressModel;
        protected readonly List<T> _activeEnemies;
        
        protected PoolGameObject _poolViews;
        protected Vector2 _bottomRight;
        protected float _borderLength;
        protected Vector2 _topLeft;
        protected float _borderHeight;

        protected float _spawnDelay;
        protected float _pastTime;
        
        public EnemyFactoryAbstract(IEnemiesConfig enemiesConfig, IUpdateProvider updateProvider, IProgressModel progressModel)
        {
            _progressModel = progressModel;
            _enemiesConfig = enemiesConfig;
            _updateProvider = updateProvider;
            _activeEnemies = new List<T>();
            
            float width = Camera.main.pixelWidth;
            float height = Camera.main.pixelHeight;

            _bottomRight = Camera.main.ScreenToWorldPoint(new Vector2 (width, 0));
            _topLeft = Camera.main.ScreenToWorldPoint(new Vector2 (0, height));
            _borderLength = _bottomRight.x * 2;
            _borderHeight = _topLeft.y * 2;
        }

        public abstract T CreateEnemy();
        public abstract void Start();
        
        public void FixedUpdate()
        {
            if (_spawnDelay < _pastTime)
            {
                CreateEnemy();
                _pastTime = 0;
            }

            CheckLifetime();
            _pastTime += Time.fixedDeltaTime;
            
        }

        protected abstract void CheckLifetime();

        public void End()
        {
            for (int i = 0; i < _activeEnemies.Count; i++)
            {
                _activeEnemies[i].Destroy();
                i--;
            }
            
            _activeEnemies.Clear();
        }
        
        protected void ReturnToPool(IBaseModel baseModel)
        {
            if (baseModel is T model)
            {
                model.OnDestroy -= ReturnToPool;
                _activeEnemies.Remove(model);
                _poolViews.Return(model.View.gameObject);
            }
        }
        
        protected Vector2 GetStartPosition(int startBorder)
        {
            switch (startBorder)
            {
                case 0:
                    // Top
                    return new Vector2(Random.Range(_topLeft.x, _bottomRight.x), _topLeft.y);
                case 1:
                    // Right
                    return new Vector2(_bottomRight.x, Random.Range(_bottomRight.y, _topLeft.y));
                case 2:
                    // Bottom
                    return new Vector2(Random.Range(_topLeft.x, _bottomRight.x), _bottomRight.y);
                case 3:
                    // Left
                    return new Vector2(_topLeft.x, Random.Range(_bottomRight.y, _topLeft.y));
            }
            
            return new Vector2(Random.Range(_topLeft.x, _bottomRight.x), _topLeft.y);
        }
    }
}