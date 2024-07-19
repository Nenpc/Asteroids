using Asteroids.Scripts.Core.Entity;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Asteroids.Scripts.Core.Hero.View
{
    [RequireComponent(typeof(PlayerInput))]
    public sealed class HeroView : BaseView
    {
        [SerializeField] private Transform _bulletPosition;

        public Transform BulletPosition => _bulletPosition;
    }
}