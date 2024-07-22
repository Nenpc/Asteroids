using Asteroids.Scripts.Core.Enemies.Views;
using UnityEngine;

namespace Asteroids.Scripts.Core.Enemies.Config
{
    [System.Serializable]
    public sealed class EnemyConfig
    {
        [SerializeField] private Enums.Enemies _enemyType;
        [SerializeField] private int _startAmount;
        [SerializeField] private float _speed;
        [SerializeField] private int _maxCount;
        [SerializeField] private int _spawnDelaySecond;
        [SerializeField] private int _lifetimeSecond;
        [SerializeField] private EnemyView _view;

        public Enums.Enemies EnemyType => _enemyType;
        public int StartAmount => _startAmount;
        public float Speed => _speed;
        public int MaxCount => _maxCount;
        public int SpawnDelaySecond => _spawnDelaySecond;
        public int LifetimeSecond => _lifetimeSecond;
        public EnemyView View => _view;
    }
}