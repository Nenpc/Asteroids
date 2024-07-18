using UnityEngine;
using UnityEngine.InputSystem;

namespace Asteroids.Scripts.Core.Hero.View
{
    [RequireComponent(typeof(PlayerInput))]
    public sealed class HeroView : MonoBehaviour
    {
        [SerializeField] private Transform _bulletPosition;

        private Transform BulletPosition => _bulletPosition;
    }
}