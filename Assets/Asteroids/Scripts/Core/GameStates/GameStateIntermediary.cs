using System.Collections.Generic;
using Asteroids.Infrastructure.Intermediary;
using Asteroids.Scripts.Core.Enums;

namespace Asteroids.Scripts.Core.GamesState
{
    public class GameStateIntermediary : IntermediaryAbstract<GameStates>
    {
        private readonly IEnumerable<IIntermediaryState<GameStates>> _gameStates;
        
        protected override GameStates FirstState => GameStates.Menu;
        protected override IEnumerable<IIntermediaryState<GameStates>> States => _gameStates;

        public GameStateIntermediary(IEnumerable<IIntermediaryState<GameStates>> gameStates)
        {
            _gameStates = gameStates;
        }
    }
}