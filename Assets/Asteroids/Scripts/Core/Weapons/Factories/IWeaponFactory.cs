using Asteroids.Scripts.Core.Base;

namespace Asteroids.Scripts.Core.Weapons.Model
{
    public interface IWeaponFactory<T> where T : WeaponModelBase
    {
        void CreateWeapon(IShooter owner);
        
        void Start();

        void FixedUpdate();

        void End();
    }
}