using Asteroids.Scripts.Core.Base;

namespace Asteroids.Scripts.Core.Weapons.Model
{
    public interface IWeaponsCreator
    {
        void CreateWeapon(Enums.Weapons weaponType, IShooter shooter);
        void Start();
        void FixedUpdate();
        void End();
    }
}