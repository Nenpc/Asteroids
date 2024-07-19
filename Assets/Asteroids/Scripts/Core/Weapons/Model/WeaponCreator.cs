using System.Collections.Generic;
using Asteroids.Scripts.Core.Entity;
using Asteroids.Scripts.Core.Weapons.Config;
using Asteroids.Scripts.Core.Weapons.View;
using Asteroids.Scripts.Infrastructure.Pools;
using Asteroids.Scripts.Infrastructure.UpdateProvider;
using UnityEngine;

namespace Asteroids.Scripts.Core.Weapons.Model
{
    public sealed class WeaponCreator : IWeaponCreator
    {
        private readonly IWeaponsConfig _weaponsConfig;
        private readonly IUpdateProvider _updateProvider;
        public Dictionary<Enums.Weapons, PoolGameObject> _poolViews;
        
        public WeaponCreator(IWeaponsConfig weaponsConfig, IUpdateProvider updateProvider)
        {
            _weaponsConfig = weaponsConfig;
            _updateProvider = updateProvider;
            _poolViews = new Dictionary<Enums.Weapons, PoolGameObject>();
        }

        public void CreateWeapon(Enums.Weapons weaponType, BaseModel owner, Transform startTransform, Quaternion rotation, bool glue = false)
        {
            if (!_poolViews.ContainsKey(weaponType))
            {
                var config = _weaponsConfig.GetWeapon(weaponType);
                _poolViews.Add(weaponType, new PoolGameObject(config.View.gameObject, config.MaxCount));
            }
            
            var weaponView = _poolViews[weaponType].Get().GetComponent<WeaponView>();
            var model = GetModel(weaponType, weaponView, owner);
            model.OnDestroy += ReturnToPool;

            weaponView.transform.position = startTransform.position;
            if (glue)
                weaponView.transform.SetParent(startTransform);
            else
                weaponView.transform.rotation = rotation;
            
            weaponView.gameObject.SetActive(true);
        }

        private WeaponModelAbstract GetModel(Enums.Weapons weaponType, WeaponView view, BaseModel owner)
        {
            switch (weaponType)
            {
                case Enums.Weapons.Bullet:
                    return new WeaponBullet(view, owner, _updateProvider);
                case Enums.Weapons.Laser:
                    return new WeaponLaser(view, owner, _updateProvider);
                default:
                    Debug.LogError($"Have't model script for weapon type {weaponType}!");
                    return new WeaponBullet(view, owner, _updateProvider);
            }
        }

        private void ReturnToPool(BaseModel baseModel)
        {
            if (baseModel is WeaponModelAbstract weaponModelAbstract)
            {
                _poolViews[weaponModelAbstract.WeaponType].Return(weaponModelAbstract.View.gameObject);
            }
            else
            {
                Debug.LogError($"Can't put model {baseModel.ModelType} to weapon pool!");
            }
        }
    }
}