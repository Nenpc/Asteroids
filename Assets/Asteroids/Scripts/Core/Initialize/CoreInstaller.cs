using Asteroids.Infrastructure.Intermediary;
using Asteroids.Scripts.Core.Enums;
using Asteroids.Scripts.Core.GamesState.Fight;
using Asteroids.Scripts.Core.GamesState.GameOver;
using Asteroids.Scripts.Core.GamesState.Menu;
using Zenject;

namespace Asteroids.Core.Initialize
{
    public sealed class CoreInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindGameSate();
            
            Container.Bind<IInitializable>().To<GameInitializer>().AsSingle().NonLazy();
            Container.Bind<IInitializable>().To<AdditionalInitializer>().AsSingle().NonLazy();
        }

        private void BindGameSate()
        {
            Container.Bind<IIntermediaryState<GameStates>>().To<GameStateMenu>().AsSingle().NonLazy();
            Container.Bind<IIntermediaryState<GameStates>>().To<GameStateFight>().AsSingle().NonLazy();
            Container.Bind<IIntermediaryState<GameStates>>().To<GameStateGameOver>().AsSingle().NonLazy();
        }
    }
}