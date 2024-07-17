using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Scripts.Core.Enemies.Config
{
    [CreateAssetMenu(menuName = "Asteroids/Enemy/EnemiesConfig", fileName = "EnemiesConfig")]
    public sealed class EnemiesConfig : ScriptableObject, IEnemiesConfig
    {
        [SerializeField] private List<EnemyConfig> _enemyConfigs;

        public EnemyConfig GetEnemyView(Enums.Enemies enemyType)
        {
            for (int i = 0; i < _enemyConfigs.Count; i++)
            {
                if (_enemyConfigs[i].EnemyType == enemyType)
                {
                    return _enemyConfigs[i];
                }
            }
            
            Debug.LogError("Have no entity for enemy type!");
            return null;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            for (int i = 0; i < _enemyConfigs.Count; i++)
            {
                if (_enemyConfigs[i] == null)
                {
                    Debug.LogError("Enemy config have null value!");
                    return;
                }
                for (int j = i; j < _enemyConfigs.Count; j++)
                {
                    if (_enemyConfigs[j] == null)
                    {
                        Debug.LogError("Enemy config have null value!");
                        return;
                    }
                    
                    if (_enemyConfigs[i].EnemyType == _enemyConfigs[j].EnemyType)
                    {
                        Debug.LogError("Have equals enemy type!");
                        return;
                    }   
                }
            }
        }
#endif
    }
}