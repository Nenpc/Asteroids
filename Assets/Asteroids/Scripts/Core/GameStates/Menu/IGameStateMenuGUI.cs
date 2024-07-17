using UnityEngine.UI;

namespace Asteroids.Scripts.Core.GamesState.Menu
{
    public interface IGameStateMenuGUI : IGameStateGUIBase
    {
        public Button StartGame { get; }
        public Button Quite { get; }
    }
}