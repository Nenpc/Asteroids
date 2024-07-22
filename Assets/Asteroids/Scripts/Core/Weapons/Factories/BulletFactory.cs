using Asteroids.Scripts.Core.Base;
using Asteroids.Scripts.Core.Weapons.Config;
using Asteroids.Scripts.Core.Weapons.Model;
using Asteroids.Scripts.Core.Weapons.View;
using Asteroids.Scripts.Infrastructure.Pools;
using Asteroids.Scripts.Infrastructure.UpdateProvider;

namespace Asteroids.Scripts.Core.Weapons.Factories
{
    public sealed class BulletFactory : IWeaponFactory<WeaponBullet>
    {
        private Enums.Weapons WeaponType => Enums.Weapons.Bullet;
        
        private readonly IWeaponsConfig _weaponsConfig;
        private readonly IUpdateProvider _updateProvider;
        private PoolGameObject _poolViews;
        
        public BulletFactory(IWeaponsConfig weaponsConfig, IUpdateProvider updateProvider)
        {
            _weaponsConfig = weaponsConfig;
            _updateProvider = updateProvider;
            var config = _weaponsConfig.GetWeapon(WeaponType);
            _poolViews = new PoolGameObject(config.View.gameObject, config.MaxCount);
        }

        public void CreateWeapon(IShooter owner)
        {
            var weaponView = _poolViews.Get().GetComponent<WeaponView>();
            var model = new WeaponBullet(weaponView, owner, _updateProvider);;
            weaponView.Init(model);
            model.OnDestroy += ReturnToPool;

            weaponView.transform.position = owner.BulletStartPosition.position;
            weaponView.transform.rotation = owner.View.transform.rotation;
            
            weaponView.gameObject.SetActive(true);
        }

        public void Start()
        {

        }

        public void FixedUpdate()
        {
        }

        public void End()
        {
        }

        private void ReturnToPool(IBaseModel baseModel)
        {
            if (baseModel is WeaponBullet weaponBullet)
            {
                _poolViews.Return(weaponBullet.View.gameObject);
            }
        }
    }
}