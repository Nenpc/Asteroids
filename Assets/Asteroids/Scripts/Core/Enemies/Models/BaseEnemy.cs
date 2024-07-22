using System;
using Asteroids.Scripts.Core.Base;
using Asteroids.Scripts.Core.Enemies.Views;
using Asteroids.Scripts.Infrastructure.UpdateProvider;
using UnityEngine;

namespace Asteroids.Scripts.Core.Enemies.Models
{
    public abstract class BaseEnemy : IBaseModel
    {
        public event Action<IBaseModel> OnDestroy;
        
        public abstract Enums.Models ModelType { get; }
        public abstract Enums.Enemies EnemyType { get; }
        public float Lifetime => _lifetime;
        public BaseView View => _view;

        private readonly EnemyView _view;
        private readonly IUpdateProvider _updateProvider;
        protected float _lifetime;
        
        public BaseEnemy(EnemyView enemyView, IUpdateProvider updateProvider)
        {
            _view = enemyView;
            _updateProvider = updateProvider;
            _view.Init(this);
            _view.OnTrigger += OnTriggerEnter;
            _updateProvider.OnFixedUpdate += FixedUpdate;
        }

        protected abstract void OnTriggerEnter(Collider2D obj);
        
        public abstract void FixedUpdate();

        public void Destroy()
        {
            OnDestroy?.Invoke(this);
            _view.OnTrigger -= OnTriggerEnter;
            _updateProvider.OnFixedUpdate -= FixedUpdate;
        }
    }
}