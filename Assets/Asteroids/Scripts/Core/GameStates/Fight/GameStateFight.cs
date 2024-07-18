using System;
using Asteroids.Scripts.Core.Enums;
using Asteroids.Infrastructure.Intermediary;
using Asteroids.Scripts.Core.GamesState.Configs;
using Asteroids.Scripts.Core.Hero.Configs;
using Asteroids.Scripts.Core.Hero.Models;
using Asteroids.Scripts.Infrastructure.UpdateProvider;
using UnityEngine;

namespace Asteroids.Scripts.Core.GamesState.Fight
{
    public sealed class GameStateFight : IIntermediaryState<GameStates>, IDisposable
    {
        public event Action<GameStates> ChangeStateAction;
        
        public GameStates State => GameStates.Fight;
        
        private IGameStateFightGUI _gui;
        private readonly IGameStatesConfig _config;
        private readonly IUpdateProvider _updateProvider;
        private readonly IHeroModel _heroModel;
        
        public GameStateFight(IGameStatesConfig config, 
            IUpdateProvider updateProvider,
            IHeroModel heroModel)
        {
            _config = config;
            _updateProvider = updateProvider;
            _heroModel = heroModel;
        }
        
        public void Dispose()
        {
            _gui?.Continue.onClick.RemoveAllListeners();
            _gui?.Quite.onClick.RemoveAllListeners();
        }
        
        private void CreateGUI()
        {
            _gui = GameObject.Instantiate(_config.GetGameStateConfigView(State)).GetComponent<IGameStateFightGUI>();
        }

        private void CreateHero()
        {
            _heroModel.Start();
        }
        
        private void CreateEnemies()
        {
        }

        public void Start()
        {
            CreateGUI();
            _gui.Show();
            _gui.Continue.onClick.AddListener(() => ChangeStateAction?.Invoke(GameStates.GameOver));
            _gui.Quite.onClick.AddListener(EndGame);

            CreateHero();
            CreateEnemies();
            
            _updateProvider.OnFixedUpdate += FixedUpdate;
        }

        private void FixedUpdate()
        {
            _heroModel.FixedUpdate();
        }

        private void EndGame()
        {
            Application.Quit();
        }

        public void End()
        {
            _updateProvider.OnUpdate -= FixedUpdate;
            
            _heroModel.Destroy();
            
            _gui.Hide();
            _gui.Destroy();
            _gui = null;
        }
    }
}