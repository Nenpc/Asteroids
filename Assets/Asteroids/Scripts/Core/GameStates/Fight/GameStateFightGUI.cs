using Asteroids.Scripts.Core.Enums;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Asteroids.Scripts.Core.GamesState.Fight
{
    public sealed class GameStateFightGUI : GameStateGUIAbstract, IGameStateFightGUI
    {
        [Header("Game info")]
        [SerializeField] private TextMeshProUGUI _coordinate;
        [SerializeField] private TextMeshProUGUI _rotation;
        [SerializeField] private TextMeshProUGUI _speed;
        [SerializeField] private TextMeshProUGUI _laserAmount;
        [SerializeField] private TextMeshProUGUI _laserCooldown;
        
        [Header("Pause panel")]
        [SerializeField] private GameObject _pausePanel;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _quiteButton;
        
        public override GameStates States => GameStates.Fight;

        public Button Continue => _continueButton;
        public Button Quite => _quiteButton;
        
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
        
        public void Destroy()
        {
            GameObject.Destroy(gameObject);
        }
    }
}