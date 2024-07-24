using System.Collections.Generic;
using Asteroids.Infrastructure.Intermediary;
using Asteroids.Scripts.Core.Enemies.Config;
using Asteroids.Scripts.Core.Enemies.Models;
using Asteroids.Scripts.Core.Enums;
using Asteroids.Scripts.Core.GamesState;
using Asteroids.Scripts.Core.GamesState.Configs;
using Asteroids.Scripts.Core.GamesState.Fight;
using Asteroids.Scripts.Core.GamesState.GameOver;
using Asteroids.Scripts.Core.GamesState.Menu;
using Asteroids.Scripts.Core.Hero.Configs;
using Asteroids.Scripts.Core.Hero.Models;
using Asteroids.Scripts.Core.Progress.Models;
using Asteroids.Scripts.Core.Weapons.Config;
using Asteroids.Scripts.Core.Weapons.Factories;
using Asteroids.Scripts.Core.Weapons.Model;
using Asteroids.Scripts.Infrastructure.UpdateProvider;
using UnityEngine;

namespace Asteroids.Core.Initialize
{
    public sealed class CoreInstaller : MonoBehaviour
    {
        [SerializeField] private WeaponsConfig _weaponsConfig;
        [SerializeField] private HeroConfig _heroConfig;
        [SerializeField] private EnemiesConfig _enemiesConfig;
        [SerializeField] private GameStatesConfig _gameStatesConfig;
        
        [SerializeField] private UpdateProvider _updateProvider;

        private UserInput _userInput;
        private ProgressModel _progressModel;
        private GameInitializer _gameInitializer;

        private void Awake()
        {
            InstallBindings();
        }

        private void InstallBindings()
        {
            _userInput = new UserInput();
            _progressModel = new ProgressModel();
            
            BindWeapon();
            BindHero();
            BindEnemy();
            BindGameSate();

            _gameInitializer = new GameInitializer(_gameStateIntermediary);
        }

        private GameStateMenu _gameStateMenu;
        private GameStateFight _gameStateFight;
        private GameStateGameOver _gameStateGameOver;

        private GameStateIntermediary _gameStateIntermediary;
        private List<IIntermediaryState<GameStates>> _gameStates;
        
        private void BindGameSate()
        {
            _gameStates = new List<IIntermediaryState<GameStates>>();
            _gameStateMenu = new GameStateMenu(_gameStatesConfig);
            _gameStates.Add(_gameStateMenu);
            _gameStateFight = new GameStateFight(_gameStatesConfig, _updateProvider, _heroModel, _enemiesCreator, _weaponsCreator, _laserFactory, _userInput);
            _gameStates.Add(_gameStateFight);
            _gameStateGameOver = new GameStateGameOver(_gameStatesConfig, _progressModel, _heroModel, _enemiesCreator);
            _gameStates.Add(_gameStateGameOver);

            _gameStateIntermediary = new GameStateIntermediary(_gameStates);
        }

        private HeroModel _heroModel;
        private void BindHero()
        {
            _heroModel = new HeroModel(_heroConfig, _weaponsCreator, _userInput);
        }
        
        private AsteroidSmallFactory _asteroidSmallFactory;
        private AsteroidBigFactory _asteroidBigFactory;
        private UFOFactory _ufoFactory;
        private EnemiesCreator _enemiesCreator;
        
        private void BindEnemy()
        {
            _asteroidSmallFactory = new AsteroidSmallFactory(_enemiesConfig, _updateProvider, _progressModel);
            _asteroidBigFactory = new AsteroidBigFactory(_asteroidSmallFactory, _enemiesConfig, _updateProvider, _progressModel);
            _ufoFactory = new UFOFactory(_heroModel, _enemiesConfig, _updateProvider, _progressModel);
            _enemiesCreator = new EnemiesCreator(_asteroidSmallFactory, _asteroidBigFactory, _ufoFactory);
        }

        private LaserFactory _laserFactory;
        private IWeaponFactory<WeaponBullet> _bulletFactory;
        private IWeaponsCreator _weaponsCreator;
        private void BindWeapon()
        {
            _laserFactory = new LaserFactory(_weaponsConfig, _updateProvider);
            _bulletFactory = new BulletFactory(_weaponsConfig, _updateProvider);
            _weaponsCreator = new WeaponsCreator(_laserFactory, _bulletFactory);
        }
    }
}