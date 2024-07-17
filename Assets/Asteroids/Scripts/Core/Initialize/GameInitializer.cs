using Asteroids.Infrastructure.Intermediary;
using Asteroids.Scripts.Core.Enums;
using Zenject;

namespace Asteroids.Core.Initialize
{
    public sealed class GameInitializer : IInitializable
    {
        private readonly IIntermediary<GameStates> _gameStateIntermediary;

        public GameInitializer(IIntermediary<GameStates> gameStateIntermediary)
        {
            _gameStateIntermediary = gameStateIntermediary;
        }

        public void Initialize()
        {
            _gameStateIntermediary.StartIntermediaryStates();
        }
    }
}