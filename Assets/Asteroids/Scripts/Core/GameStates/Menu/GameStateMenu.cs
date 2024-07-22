using System;
using Asteroids.Scripts.Core.Enums;
using Asteroids.Infrastructure.Intermediary;
using Asteroids.Scripts.Core.GamesState.Configs;
using UnityEngine;

namespace Asteroids.Scripts.Core.GamesState.Menu
{
    public sealed class GameStateMenu : IIntermediaryState<GameStates>, IDisposable
    {
        public event Action<GameStates> ChangeStateAction;
        
        public GameStates State => GameStates.Menu;

        private IGameStateMenuGUI _gui;
        private readonly IGameStatesConfig _config;
        
        public GameStateMenu(IGameStatesConfig config)
        {
            _config = config;
        }

        private void CreateGUI()
        {
            _gui = GameObject.Instantiate(_config.GetGameStateConfigView(State)).GetComponent<IGameStateMenuGUI>();
        }

        public void Dispose()
        {
            _gui?.StartGame.onClick.RemoveAllListeners();
            _gui?.Quit.onClick.RemoveAllListeners();
            _gui?.Destroy();
        }

        public void Start()
        {
            CreateGUI();
            _gui.Show();
            _gui.StartGame.onClick.AddListener(() => ChangeStateAction?.Invoke(GameStates.Fight));
            _gui.Quit.onClick.AddListener(EndGame);
        }
        
        private void EndGame()
        {
            Application.Quit();
        }

        public void End()
        {
            _gui.Hide();
            _gui.Destroy();
            _gui = null;
        }
    }
}