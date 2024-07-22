using Asteroids.Scripts.Core.Base;
using Asteroids.Scripts.Core.Enemies.Views;
using Asteroids.Scripts.Core.Progress.Interfeces;
using Asteroids.Scripts.Core.Weapons.Model;
using Asteroids.Scripts.Infrastructure.UpdateProvider;
using UnityEngine;

namespace Asteroids.Scripts.Core.Enemies.Models
{
    public sealed class AsteroidBigEnemy: BaseEnemy
    {
        private const int MaxAsteroidsAfterDeath = 4;
        private const int MinAsteroidsAfterDeath = 2;
        public override Enums.Models ModelType => Enums.Models.Enemy;
        public override Enums.Enemies EnemyType => Enums.Enemies.AsteroidSmall;
        
        private float _speed;
        
        private readonly ICreateAsteroidPosition _createAsteroidPosition;
        private readonly IProgressModel _progressModel;

        public AsteroidBigEnemy(EnemyView enemyView, 
            float speed, 
            ICreateAsteroidPosition createAsteroidPosition, 
            IUpdateProvider updateProvider, 
            IProgressModel progressModel) : base(enemyView, updateProvider)
        {
            _createAsteroidPosition = createAsteroidPosition;
            _progressModel = progressModel;
            _speed = speed;
        }

        protected override void OnTriggerEnter(Collider2D obj)
        {
            if (obj.gameObject.TryGetComponent<BaseView>(out var baseView))
            {
                if (baseView.Model.ModelType == Enums.Models.Weapon)
                {
                    if (baseView.Model is WeaponModelBase weaponModelAbstract && weaponModelAbstract.WeaponType != Enums.Weapons.Laser)
                        _createAsteroidPosition.CreateOnPosition(View.transform, Random.Range(MinAsteroidsAfterDeath, MaxAsteroidsAfterDeath));
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