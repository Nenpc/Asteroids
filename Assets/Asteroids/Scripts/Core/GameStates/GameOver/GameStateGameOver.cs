using System;
using Asteroids.Scripts.Core.Enums;
using Asteroids.Infrastructure.Intermediary;
using Asteroids.Scripts.Core.Enemies.Models;
using Asteroids.Scripts.Core.GamesState.Configs;
using Asteroids.Scripts.Core.GamesState.Menu;
using Asteroids.Scripts.Core.Hero.Models;
using Asteroids.Scripts.Core.Progress.Interfeces;
using UnityEngine;

namespace Asteroids.Scripts.Core.GamesState.GameOver
{
    public sealed class GameStateGameOver : IIntermediaryState<GameStates>, IDisposable
    {
        public event Action<GameStates> ChangeStateAction;

        public GameStates State => GameStates.GameOver;

        private IGameStateGameOverGUI _gui;
        private readonly IHeroModel _heroModel;
        private readonly IEnemiesCreator _enemiesCreator;
        private readonly IGameStatesConfig _config;
        private readonly IProgressModel _progressModel;

        public GameStateGameOver(IGameStatesConfig config, 
            IProgressModel progressModel,
            IHeroModel heroModel, 
            IEnemiesCreator enemiesCreator)
        {
            _config = config;
            _progressModel = progressModel;
            _heroModel = heroModel;
            _enemiesCreator = enemiesCreator;
        }

        private void CreateGUI()
        {
            _gui = GameObject.Instantiate(_config.GetGameStateConfigView(State)).GetComponent<IGameStateGameOverGUI>();
            _gui.Init(_progressModel.GetScore);
        }

        public void Dispose()
        {
            _gui?.RestartGame.onClick.RemoveAllListeners();
            _gui?.Quit.onClick.RemoveAllListeners();
            _gui?.Destroy();
        }

        public void Start()
        {
            CreateGUI();
            _gui.Show();
            _gui.RestartGame.onClick.AddListener(Restart);
            _gui.Quit.onClick.AddListener(EndGame);
        }

        private void Restart()
        {
            _progressModel.ResetScore();
            _heroModel.Destroy();
            _enemiesCreator.End();
            ChangeStateAction?.Invoke(GameStates.Fight);
        }

        private void EndGame()
        {
            _progressModel.ResetScore();
            _heroModel.Destroy();
            _enemiesCreator.End();
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