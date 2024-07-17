using System;
using Asteroids.Scripts.Core.Enums;
using Asteroids.Infrastructure.Intermediary;
using Asteroids.Scripts.Core.GamesState.Configs;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Asteroids.Scripts.Core.GamesState.Menu
{
    public sealed class GameStateMenu : IIntermediaryState<GameStates>, IDisposable
    {
        public event Action<GameStates> OnEndState;
        
        public GameStates State => GameStates.Menu;

        private IGameStateMenuGUI _gui;
        private readonly GameStatesConfig _config;
        
        public GameStateMenu(GameStatesConfig config)
        {
            _config = config;
            LoadGUIAsync().Forget();
        }

        private async UniTask LoadGUIAsync()
        {
            _gui.StartGame.onClick.AddListener(() => OnEndState?.Invoke(State));
            _gui.Quite.onClick.AddListener(EndGame);
        }

        public void Dispose()
        {
            _gui?.StartGame.onClick.RemoveAllListeners();
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