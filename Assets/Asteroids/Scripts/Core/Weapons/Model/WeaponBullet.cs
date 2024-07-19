using System;
using System.Data;
using Asteroids.Scripts.Core.Entity;
using Asteroids.Scripts.Core.Weapons.View;
using Asteroids.Scripts.Infrastructure.UpdateProvider;
using UnityEngine;

namespace Asteroids.Scripts.Core.Weapons.Model
{
    public sealed class WeaponBullet : WeaponModelAbstract
    {
        public Enums.Weapons WeaponType => Enums.Weapons.Bullet;
        private float _speed = 6f;
        private TimeSpan _lifeTime = new TimeSpan(0, 0, 2);
        private DateTime _startTime;
        public WeaponBullet(WeaponView weaponView, BaseModel owner, IUpdateProvider updateProvider) : base(weaponView, owner, updateProvider)
        {
            _startTime = DateTime.Now;
        }

        protected override void OnTriggerEnter(Collider obj)
        {
            Debug.Log(obj.name);
        }

        public override void FixedUpdate()
        {
            View.transform.Translate((_speed * Time.fixedDeltaTime) * Vector2.up, Space.Self);
            if (_startTime + _lifeTime < DateTime.Now)
                Destroy();
        }
    }
}