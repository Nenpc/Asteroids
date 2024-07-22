using Asteroids.Scripts.Core.Base;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Asteroids.Scripts.Core.Hero.View
{
    [RequireComponent(typeof(PlayerInput))]
    public sealed class HeroView : BaseView
    {
        [SerializeField] private Transform _bulletPosition;
        [SerializeField] private GameObject _fire;

        public Transform BulletPosition => _bulletPosition;
        public GameObject Fire => _fire;
    }
}