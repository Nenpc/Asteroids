using Asteroids.Scripts.Core.Enemies.Views;
using UnityEngine;

namespace Asteroids.Scripts.Core.Enemies.Config
{
    [System.Serializable]
    public sealed class EnemyConfig
    {
        [SerializeField] private Enums.Enemies _enemyType;
        [SerializeField] private EnemyView _view;

        public Enums.Enemies EnemyType => _enemyType;
        public EnemyView View => _view;
    }
}