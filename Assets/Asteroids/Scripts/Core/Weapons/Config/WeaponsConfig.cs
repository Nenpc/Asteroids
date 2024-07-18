using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Scripts.Core.Weapons.Config
{
    [CreateAssetMenu(menuName = "Asteroids/Weapon/WeaponConfig", fileName = "WeaponConfig")]
    public sealed class WeaponsConfig : ScriptableObject, IWeaponsConfig
    {
        [SerializeField] private List<WeaponConfig> _weaponConfigs;

        public WeaponConfig GetWeapon(Enums.Weapons weaponType)
        {
            for (int i = 0; i < _weaponConfigs.Count; i++)
            {
                if (_weaponConfigs[i].WeaponType == weaponType)
                {
                    return _weaponConfigs[i];
                }
            }
            
            Debug.LogError("Have no entity for weapon type!");
            return null;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            for (int i = 0; i < _weaponConfigs.Count - 1; i++)
            {
                if (_weaponConfigs[i] == null)
                {
                    Debug.LogError("Weapon config have null value!");
                    return;
                }
                for (int j = i + 1; j < _weaponConfigs.Count; j++)
                {
                    if (_weaponConfigs[j] == null)
                    {
                        Debug.LogError("Weapon config have null value!");
                        return;
                    }
                    
                    if (_weaponConfigs[i].WeaponType == _weaponConfigs[j].WeaponType)
                    {
                        Debug.LogError("Have equals weapon type!");
                        return;
                    }   
                }
            }
        }
#endif
    }
}