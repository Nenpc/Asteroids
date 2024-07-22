using System;
using Asteroids.Scripts.Core.Base;
using Asteroids.Scripts.Core.Weapons.View;
using Asteroids.Scripts.Infrastructure.UpdateProvider;
using UnityEngine;

namespace Asteroids.Scripts.Core.Weapons.Model
{
    public sealed class WeaponBullet : WeaponModelBase
    {
        public override Enums.Weapons WeaponType => Enums.Weapons.Bullet;
        private float _speed = 6f;
        private TimeSpan _lifeTime = new TimeSpan(0, 0, 2);
        private DateTime _startTime;
        public WeaponBullet(WeaponView weaponView, IShooter owner, IUpdateProvider updateProvider) : 
            base(weaponView, owner, updateProvider)
        {
            _startTime = DateTime.Now;
        }

        protected override void OnTriggerEnter(Collider2D obj)
        {
            if (obj.gameObject.TryGetComponent<BaseView>(out var baseView))
            {
                if (baseView.Model.ModelType == Enums.Models.Enemy)
                {
                    Destroy();
                }
            }
        }

        public override void FixedUpdate()
        {
            View.transform.Translate((_speed * Time.fixedDeltaTime) * Vector2.up, Space.Self);
            if (_startTime + _lifeTime < DateTime.Now)
                Destroy();
        }
    }
}