using System.Collections.Generic;
using Asteroids.Scripts.Core.Enums;
using UnityEngine;

namespace Asteroids.Scripts.Core.GamesState.Configs
{
    [CreateAssetMenu(menuName = "Asteroids/GameState/GameStatesConfig", fileName = "GameStatesConfig")]
    public sealed class GameStatesConfig : ScriptableObject, IGameStatesConfig
    {
        [SerializeField] private List<GameStateConfig> _gameStateConfigs;

        public GameStateConfig GetGameStateConfigView(GameStates gameStatesType)
        {
            for (int i = 0; i < _gameStateConfigs.Count; i++)
            {
                if (_gameStateConfigs[i].GameStateType == gameStatesType)
                {
                    return _gameStateConfigs[i];
                }
            }

            Debug.LogError($"Have no view for {gameStatesType}!");
            return null;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            for (int i = 0; i < _gameStateConfigs.Count; i++)
            {
                if (_gameStateConfigs[i] == null)
                {
                    Debug.LogError("Game states config have null value!");
                    return;
                }

                for (int j = i; j < _gameStateConfigs.Count; j++)
                {
                    if (_gameStateConfigs[j] == null)
                    {
                        Debug.LogError("Game states config have null value!");
                        return;
                    }

                    if (_gameStateConfigs[i].GameStateType == _gameStateConfigs[j].GameStateType)
                    {
                        Debug.LogError("Have equals game states config!");
                        return;
                    }
                }
            }
        }
    }
#endif
}