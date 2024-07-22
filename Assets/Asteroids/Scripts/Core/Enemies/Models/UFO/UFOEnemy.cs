using System;
using Asteroids.Scripts.Core.Base;
using Asteroids.Scripts.Core.Enemies.Views;
using Asteroids.Scripts.Core.Hero.Models;
using Asteroids.Scripts.Core.Progress.Interfeces;
using Asteroids.Scripts.Infrastructure.UpdateProvider;
using UnityEngine;

namespace Asteroids.Scripts.Core.Enemies.Models
{
    public sealed class UFOEnemy : BaseEnemy
    {
        public override Enums.Models ModelType => Enums.Models.Enemy;
        public override Enums.Enemies EnemyType => Enums.Enemies.UFO;

        private readonly IHeroModel _heroModel;
        private readonly float _speed;
        private readonly IProgressModel _progressModel;
        
        public UFOEnemy(IHeroModel heroModel, 
            float speed, 
            EnemyView enemyView, 
            IUpdateProvider updateProvider, 
            IProgressModel progressModel) : base(enemyView, updateProvider)
        {
            _speed = speed;
            _progressModel = progressModel;
            _heroModel = heroModel;
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
            View.transform.position = Vector3.MoveTowards(
                View.transform.position, 
                _heroModel.View.transform.position, 
                _speed * Time.fixedDeltaTime);
            _lifetime += Time.fixedDeltaTime;
        }
    }
}