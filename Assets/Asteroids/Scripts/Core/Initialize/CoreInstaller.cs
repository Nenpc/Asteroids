using Asteroids.Infrastructure.Intermediary;
using Asteroids.Scripts.Core.Enemies.Models;
using Asteroids.Scripts.Core.Enums;
using Asteroids.Scripts.Core.GamesState;
using Asteroids.Scripts.Core.GamesState.Fight;
using Asteroids.Scripts.Core.GamesState.GameOver;
using Asteroids.Scripts.Core.GamesState.Menu;
using Asteroids.Scripts.Core.Hero.Models;
using Asteroids.Scripts.Core.Progress.Interfeces;
using Asteroids.Scripts.Core.Progress.Models;
using Asteroids.Scripts.Core.Weapons.Factories;
using Asteroids.Scripts.Core.Weapons.Model;
using Asteroids.Scripts.Infrastructure.UpdateProvider;
using UnityEngine;
using Zenject;

namespace Asteroids.Core.Initialize
{
    public sealed class CoreInstaller : MonoInstaller
    {
        [SerializeField] private UpdateProvider _updateProvider;
        public override void InstallBindings()
        {
            BindHero();
            BindGameSate();
            BindEnemy();
            BindWeapon();

            Container.Bind<UserInput>().FromInstance(new UserInput());
            Container.Bind<IUpdateProvider>().FromInstance(_updateProvider);
            Container.Bind<IInitializable>().To<GameInitializer>().AsSingle().NonLazy();
            Container.Bind<IProgressModel>().To<ProgressModel>().AsSingle().NonLazy();
        }

        private void BindGameSate()
        {
            Container.Bind<IIntermediaryState<GameStates>>().To<GameStateMenu>().AsSingle().NonLazy();
            Container.Bind<IIntermediaryState<GameStates>>().To<GameStateFight>().AsSingle().NonLazy();
            Container.Bind<IIntermediaryState<GameStates>>().To<GameStateGameOver>().AsSingle().NonLazy();
            
            Container.Bind<IIntermediary<GameStates>>().To<GameStateIntermediary>().AsSingle().NonLazy();
        }
        
        private void BindHero()
        {
            Container.Bind<IHeroModel>().To<HeroModel>().AsSingle().NonLazy();
        }
        
        private void BindEnemy()
        {
            Container.Bind(typeof(IEnemyFactory<AsteroidSmallEnemy>), typeof(ICreateAsteroidPosition)).To<AsteroidSmallFactory>().AsSingle().NonLazy();
            Container.Bind<IEnemyFactory<UFOEnemy>>().To<UFOFactory>().AsSingle().NonLazy();
            Container.Bind<IEnemyFactory<AsteroidBigEnemy>>().To<AsteroidBigFactory>().AsSingle().NonLazy();
            Container.Bind<IEnemiesCreator>().To<EnemiesCreator>().AsSingle().NonLazy();
        }
        
        private void BindWeapon()
        {
            Container.Bind(typeof(IWeaponFactory<WeaponLaser>), typeof(ILaserFactoryInfo)).To<LaserFactory>().AsSingle().NonLazy();
            Container.Bind<IWeaponFactory<WeaponBullet>>().To<BulletFactory>().AsSingle().NonLazy();
            Container.Bind<IWeaponsCreator>().To<WeaponsCreator>().AsSingle().NonLazy();
        }
    }
}