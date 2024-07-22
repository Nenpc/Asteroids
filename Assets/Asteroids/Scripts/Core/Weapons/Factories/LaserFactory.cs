using Asteroids.Scripts.Core.Base;
using Asteroids.Scripts.Core.Weapons.Config;
using Asteroids.Scripts.Core.Weapons.Model;
using Asteroids.Scripts.Core.Weapons.View;
using Asteroids.Scripts.Infrastructure.Pools;
using Asteroids.Scripts.Infrastructure.UpdateProvider;
using UnityEngine;

namespace Asteroids.Scripts.Core.Weapons.Factories
{
    public sealed class LaserFactory : IWeaponFactory<WeaponLaser>, ILaserFactoryInfo
    {
        private Enums.Weapons WeaponType => Enums.Weapons.Laser;
        public int Amount => _amount;
        public float RespawnTime => _currentRespawnTime;

        private int _amount;
        private float _respawnTime = 10;
        private float _currentRespawnTime;
        
        private readonly IWeaponsConfig _weaponsConfig;
        private readonly IUpdateProvider _updateProvider;
        private PoolGameObject _poolViews;
        
        public LaserFactory(IWeaponsConfig weaponsConfig, IUpdateProvider updateProvider)
        {
            _weaponsConfig = weaponsConfig;
            _updateProvider = updateProvider;
            var config = _weaponsConfig.GetWeapon(WeaponType);
            _poolViews = new PoolGameObject(config.View.gameObject, config.MaxCount);
        }

        public void CreateWeapon(IShooter owner)
        {
            if (_amount == 0) return;
            
            var weaponView = _poolViews.Get().GetComponent<WeaponView>();
            var model = new WeaponLaser(weaponView, owner, _updateProvider);;
            weaponView.Init(model);
            model.OnDestroy += ReturnToPool;

            weaponView.transform.position = owner.BulletStartPosition.position;
            weaponView.transform.rotation = owner.View.transform.rotation;
            
            weaponView.gameObject.SetActive(true);
            _amount = 0;
            _currentRespawnTime = _respawnTime;
        }

        public void Start()
        {
            _amount = 0;
            _currentRespawnTime = _respawnTime;
        }

        public void FixedUpdate()
        {
            if (_currentRespawnTime > 0)
            {
                _currentRespawnTime = Mathf.Clamp(_currentRespawnTime - Time.fixedDeltaTime, 0, _respawnTime);
                if (_currentRespawnTime == 0)
                {
                    _amount = 1;
                }
            }
        }

        public void End()
        {
        }

        private void ReturnToPool(IBaseModel baseModel)
        {
            if (baseModel is WeaponLaser weaponLaser)
            {
                _poolViews.Return(weaponLaser.View.gameObject);
            }
        }
        
    }
}