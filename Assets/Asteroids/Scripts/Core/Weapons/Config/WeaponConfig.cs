using Asteroids.Scripts.Core.Weapons.View;
using UnityEngine;

namespace Asteroids.Scripts.Core.Weapons.Config
{
    [System.Serializable]
    public sealed class WeaponConfig
    {
        [SerializeField] private Enums.Weapons _weaponType;
        [SerializeField] private int _maxCount;
        [SerializeField] private WeaponView _view;

        public Enums.Weapons WeaponType => _weaponType;
        public WeaponView View => _view;
        public int MaxCount => _maxCount;
    }
}