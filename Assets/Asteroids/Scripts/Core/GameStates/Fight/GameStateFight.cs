using System;
using Asteroids.Scripts.Core.Enums;
using Asteroids.Infrastructure.Intermediary;
using Asteroids.Scripts.Core.GamesState.Configs;
using UnityEngine;

namespace Asteroids.Scripts.Core.GamesState.Fight
{
    public sealed class GameStateFight : IIntermediaryState<GameStates>, IDisposable
    {
        public event Action<GameStates> OnEndState;
        
        public GameStates State => GameStates.Fight;
        
        private IGameStateFightGUI _gui;
        private readonly GameStatesConfig _config;
        
        public GameStateFight(GameStatesConfig config)
        {
            _config = config;
            _gui.Continue.onClick.AddListener(() => OnEndState?.Invoke(State));
            _gui.Quite.onClick.AddListener(EndGame);
        }
        
        public void Dispose()
        {
            _gui?.Continue.onClick.RemoveAllListeners();
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