using Asteroids.Scripts.Core.Entity;
using Asteroids.Scripts.Core.Weapons.View;
using Asteroids.Scripts.Infrastructure.UpdateProvider;
using UnityEngine;

namespace Asteroids.Scripts.Core.Weapons.Model
{
    public sealed class WeaponLaser : WeaponModelAbstract
    {
        public Enums.Weapons WeaponType => Enums.Weapons.Laser;
        
        public WeaponLaser(WeaponView weaponView, BaseModel owner, IUpdateProvider updateProvider) : base(weaponView, owner, updateProvider)
        {
        }

        protected override void OnTriggerEnter(Collider obj)
        {
            //if (obj.TryGetComponent<>())
        }

        public override void FixedUpdate()
        {
            
        }
    }
}