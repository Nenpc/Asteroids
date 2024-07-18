namespace Asteroids.Scripts.Core.Weapons.Config
{
    public interface IWeaponsConfig
    {
        WeaponConfig GetWeapon(Enums.Weapons weaponType);
    }
}