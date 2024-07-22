using UnityEngine.UI;

namespace Asteroids.Scripts.Core.GamesState.Menu
{
    public interface IGameStateMenuGUI : IGameStateGUI
    {
        public Button StartGame { get; }
        public Button Quit { get; }
    }
}