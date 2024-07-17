using Asteroids.Scripts.Core.Enums;
using UnityEngine;

namespace Asteroids.Scripts.Core.GamesState.Configs
{
    [System.Serializable]
    public sealed class GameStateConfig
    {
        [SerializeField] private GameStates _gameStateType;
        [SerializeField] private GameStateGUIAbstract _view;

        public GameStates GameStateType => _gameStateType;
        public GameStateGUIAbstract View => _view;
    }
}