using Asteroids.Infrastructure.Intermediary;
using Asteroids.Scripts.Core.Enemies.Models;
using Asteroids.Scripts.Core.Enums;
using Asteroids.Scripts.Core.GamesState;
using Asteroids.Scripts.Core.GamesState.Fight;
using Asteroids.Scripts.Core.GamesState.GameOver;
using Asteroids.Scripts.Core.GamesState.Menu;
using Asteroids.Scripts.Core.Hero.Models;
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
            BindEnemyCreator();
            BindWeaponCreator();
            
            Container.Bind<IUpdateProvider>().FromInstance(_updateProvider);
            Container.Bind<IInitializable>().To<GameInitializer>().AsSingle().NonLazy();
            Container.Bind<IInitializable>().To<AdditionalInitializer>().AsSingle().NonLazy();
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
        
        private void BindEnemyCreator()
        {
            Container.Bind<IEnemiesCreator>().To<EnemiesCreator>().AsSingle().NonLazy();
        }
        
        private void BindWeaponCreator()
        {
            Container.Bind<IWeaponCreator>().To<WeaponCreator>().AsSingle().NonLazy();
        }
    }
}