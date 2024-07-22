using UnityEngine.UI;

namespace Asteroids.Scripts.Core.GamesState.GameOver
{
    public interface IGameStateGameOverGUI : IGameStateGUI
    {
        public Button RestartGame { get; }
        public Button Quit { get; }

        void Init(int result);
    }
}