using System;
using Asteroids.Scripts.Core.Enums;
using Asteroids.Infrastructure.Intermediary;
using Asteroids.Scripts.Core.GamesState.Configs;
using UnityEngine;

namespace Asteroids.Scripts.Core.GamesState.GameOver
{
    public sealed class GameStateGameOver : IIntermediaryState<GameStates>, IDisposable
    {
        public event Action<GameStates> ChangeStateAction;

        public GameStates State => GameStates.GameOver;

        private IGameStateGameOverGUI _gui;
        private readonly IGameStatesConfig _config;

        public GameStateGameOver(IGameStatesConfig config)
        {
            //_gui = gui;
            _config = config;
            // _gui.RestartGame.onClick.AddListener(() => ChangeStateAction?.Invoke(GameStates.Fight));
            // _gui.Quite.onClick.AddListener(EndGame);
        }

        public void Dispose()
        {
            _gui?.RestartGame.onClick.RemoveAllListeners();
            _gui?.Quite.onClick.RemoveAllListeners();
        }

        public void Start()
        {
            _gui.Show();
        }

        private void EndGame()
        {
            Application.Quit();
        }

        public void End()
        {
            _gui.Hide();
        }
    }
}