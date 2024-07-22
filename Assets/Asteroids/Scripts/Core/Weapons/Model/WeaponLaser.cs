using System;
using Asteroids.Scripts.Core.Base;
using Asteroids.Scripts.Core.Weapons.View;
using Asteroids.Scripts.Infrastructure.UpdateProvider;
using UnityEngine;

namespace Asteroids.Scripts.Core.Weapons.Model
{
    public sealed class WeaponLaser : WeaponModelBase
    {
        public override Enums.Weapons WeaponType => Enums.Weapons.Laser;
        private TimeSpan _lifeTime = new TimeSpan(0, 0, 4);
        private DateTime _startTime;
        
        public WeaponLaser(WeaponView weaponView, IShooter owner, IUpdateProvider updateProvider) : 
            base(weaponView, owner, updateProvider)
        {
            _startTime = DateTime.Now;
        }

        protected override void OnTriggerEnter(Collider2D obj)
        {
        }

        public override void FixedUpdate()
        {
            View.transform.position = Owner.BulletStartPosition.transform.position;
            View.transform.rotation = Owner.View.transform.rotation;
            
            if (_startTime + _lifeTime < DateTime.Now)
                Destroy();
        }
    }
}