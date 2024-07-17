using System;
using Asteroids.Scripts.Core.Enums;
using Asteroids.Infrastructure.Intermediary;
using UnityEngine;

namespace Asteroids.Scripts.Core.GamesState.GameOver
{
    public sealed class GameStateGameOver : IIntermediaryState<GameStates>, IDisposable
    {
        public event Action<GameStates> OnEndState;
        
        public GameStates State => GameStates.GameOver;

        private readonly IGameStateGameOverGUI _gui;
        
        public GameStateGameOver(IGameStateGameOverGUI gui)
        {
            _gui = gui;
            _gui.RestartGame.onClick.AddListener(() => OnEndState?.Invoke(State));
            _gui.Quite.onClick.AddListener(EndGame);
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