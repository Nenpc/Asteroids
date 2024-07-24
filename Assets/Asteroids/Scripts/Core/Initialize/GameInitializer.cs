using Asteroids.Infrastructure.Intermediary;
using Asteroids.Scripts.Core.Enums;

namespace Asteroids.Core.Initialize
{
    public sealed class GameInitializer
    {
        private readonly IIntermediary<GameStates> _gameStateIntermediary;

        public GameInitializer(IIntermediary<GameStates> gameStateIntermediary)
        {
            _gameStateIntermediary = gameStateIntermediary;
            Initialize();
        }

        private void Initialize()
        {
            _gameStateIntermediary.InitIntermediaryStates();
        }
    }
}