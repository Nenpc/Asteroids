using Asteroids.Scripts.Core.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids.Scripts.Core.GamesState.GameOver
{
    public sealed class GameStateGameOverGUI : GameStateGUIAbstract, IGameStateGameOverGUI
    {
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private Button _restartGame;
        [SerializeField] private Button _quite;
        
        public override GameStates States => GameStates.GameOver;

        public Button RestartGame => _restartGame;
        public Button Quite => _quite;
        
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}