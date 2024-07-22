using Asteroids.Scripts.Core.Base;

namespace Asteroids.Scripts.Core.Weapons.Model
{
    public sealed class WeaponsCreator : IWeaponsCreator
    {
        private readonly IWeaponFactory<WeaponLaser> _weaponLaserFactory;
        private readonly IWeaponFactory<WeaponBullet> _weaponBulletFactory;
        
        public WeaponsCreator(IWeaponFactory<WeaponLaser> weaponLaserFactory,
            IWeaponFactory<WeaponBullet> weaponBulletFactory)
        {
            _weaponLaserFactory = weaponLaserFactory;
            _weaponBulletFactory = weaponBulletFactory;
        }

        public void CreateWeapon(Enums.Weapons weaponType, IShooter shooter)
        {
            switch (weaponType)
            {
                case Enums.Weapons.Laser:
                    _weaponLaserFactory.CreateWeapon(shooter);
                    break;
                
                case Enums.Weapons.Bullet:
                    _weaponBulletFactory.CreateWeapon(shooter);
                    break;
            }
        }

        public void Start()
        {
            _weaponLaserFactory.Start();
            _weaponBulletFactory.Start();
        }
        
        public void FixedUpdate()
        {
            _weaponLaserFactory.FixedUpdate();
            _weaponBulletFactory.FixedUpdate();
        }

        public void End()
        {
            _weaponLaserFactory.End();
            _weaponBulletFactory.End();
        }
    }
}