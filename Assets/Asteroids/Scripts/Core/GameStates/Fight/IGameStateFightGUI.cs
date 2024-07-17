using UnityEngine.UI;

namespace Asteroids.Scripts.Core.GamesState.Fight
{
    public interface IGameStateFightGUI : IGameStateGUIBase
    {
        public Button Continue { get; }
        public Button Quite { get; }
    }
}