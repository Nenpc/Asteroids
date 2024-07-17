using Asteroids.Scripts.Core.Hero.View;
using UnityEngine;

namespace Asteroids.Scripts.Core.Hero.Configs
{
    [CreateAssetMenu(menuName = "Asteroids/Hero/HeroConfig", fileName = "HeroConfig")]
    public sealed class HeroConfig : ScriptableObject, IHeroConfig
    {
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _acceleration;
        [SerializeField] private float _braking;
        [SerializeField] private HeroView _heroView;
        
        public float Acceleration => _acceleration;
        public float MaxSpeed => _maxSpeed;
        public float Braking => _braking;
        public HeroView HeroView => _heroView;
    }
}