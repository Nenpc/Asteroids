using System;
using Asteroids.Scripts.Core.Base;
using Asteroids.Scripts.Core.Enemies.Views;
using Asteroids.Scripts.Core.Progress.Interfeces;
using Asteroids.Scripts.Infrastructure.UpdateProvider;
using UnityEngine;

namespace Asteroids.Scripts.Core.Enemies.Models
{
    public sealed class AsteroidSmallEnemy : BaseEnemy
    {
        public override Enums.Models ModelType => Enums.Models.Enemy;
        public override Enums.Enemies EnemyType => Enums.Enemies.AsteroidSmall;
        
        private readonly IProgressModel _progressModel;
        private float _speed;

        public AsteroidSmallEnemy(EnemyView enemyView, 
            float speed, 
            IUpdateProvider updateProvider, 
            IProgressModel progressModel) : base(enemyView, updateProvider)
        {
            _progressModel = progressModel;
            _speed = speed;
        }

        protected override void OnTriggerEnter(Collider2D obj)
        {
            if (obj.gameObject.TryGetComponent<BaseView>(out var baseView))
            {
                if (baseView.Model.ModelType == Enums.Models.Weapon)
                {
                    _progressModel.AddScore();
                    Destroy();
                }
            }
        }

        public override void FixedUpdate()
        {
            View.transform.Translate((_speed * Time.fixedDeltaTime) * Vector2.up, Space.Self);
            _lifetime += Time.fixedDeltaTime;
        }
    }
}