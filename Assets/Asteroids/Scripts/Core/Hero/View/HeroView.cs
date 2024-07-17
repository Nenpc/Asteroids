using UnityEngine;

namespace Asteroids.Scripts.Core.Hero.View
{
    public sealed class HeroView : MonoBehaviour
    {
        [SerializeField] private Transform _bulletPosition;

        private Transform BulletPosition => _bulletPosition;
    }
}