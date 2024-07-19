using Asteroids.Scripts.Core.Entity;
using UnityEngine;

namespace Asteroids.Scripts.Core.Weapons.Model
{
    public interface IWeaponCreator
    {
        void CreateWeapon(Enums.Weapons weaponType, BaseModel owner, Transform startTransform, Quaternion rotation, bool glue = false);
    }
}