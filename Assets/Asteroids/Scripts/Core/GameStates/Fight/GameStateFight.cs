using System;
using Asteroids.Scripts.Core.Enums;
using Asteroids.Infrastructure.Intermediary;
using Asteroids.Scripts.Core.Enemies.Models;
using Asteroids.Scripts.Core.GamesState.Configs;
using Asteroids.Scripts.Core.Hero.Models;
using Asteroids.Scripts.Core.Weapons.Factories;
using Asteroids.Scripts.Core.Weapons.Model;
using Asteroids.Scripts.Infrastructure.UpdateProvider;
using UnityEngine;
using UnityEngine.InputSystem;

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
        private readonly IEnemiesCreator _enemiesCreator;
        private readonly UserInput _userInput;
        private readonly ILaserFactoryInfo _laserFactoryInfo;
        private readonly IWeaponsCreator _weaponsCreator;

        private bool _pause;
        
        public GameStateFight(IGameStatesConfig config, 
            IUpdateProvider updateProvider,
            IHeroModel heroModel,
            IEnemiesCreator enemiesCreator,
            IWeaponsCreator weaponsCreator,
            ILaserFactoryInfo laserFactoryInfo,
            UserInput userInput)
        {
            _config = config;
            _updateProvider = updateProvider;
            _heroModel = heroModel;
            _enemiesCreator = enemiesCreator;
            _laserFactoryInfo = laserFactoryInfo;
            _userInput = userInput;
            _weaponsCreator = weaponsCreator;
        }
        
        private void CreateGUI()
        {
            _gui = GameObject.Instantiate(_config.GetGameStateConfigView(State)).GetComponent<IGameStateFightGUI>();
            _gui.Init(_heroModel, _updateProvider, _laserFactoryInfo);
        }
        
        public void Start()
        {
            CreateGUI();
            _gui.Show();

            _heroModel.Start();
            _heroModel.OnDestroy += (x) => ChangeStateAction?.Invoke(GameStates.GameOver);
            
            _enemiesCreator.Start();
            
            _weaponsCreator.Start();
            
            _userInput.Player.Pause.performed += Pause;
            _userInput.Player.Pause.Enable();
            
            _updateProvider.OnFixedUpdate += FixedUpdate;
            _updateProvider.Start();
        }

        private void Pause(InputAction.CallbackContext context)
        {
            if (_updateProvider.Pause)
                _updateProvider.Start();
            else
                _updateProvider.Stop();
        }

        private void FixedUpdate()
        {
            _heroModel.FixedUpdate();
            _enemiesCreator.FixedUpdate();
            _weaponsCreator.FixedUpdate();
        }
        public void End()
        {
            _updateProvider.Stop();
            _userInput.Player.Pause.performed -= Pause;
            _userInput.Player.Pause.Disable();
            _updateProvider.OnFixedUpdate -= FixedUpdate;
        }
        
        public void Dispose()
        {
        }
    }
}