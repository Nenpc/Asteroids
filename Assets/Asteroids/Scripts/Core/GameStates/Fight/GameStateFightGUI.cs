using Asteroids.Scripts.Core.Enums;
using Asteroids.Scripts.Core.Hero.Models;
using Asteroids.Scripts.Core.Weapons.Factories;
using Asteroids.Scripts.Infrastructure.UpdateProvider;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Asteroids.Scripts.Core.GamesState.Fight
{
    public sealed class GameStateFightGUI : GameStateGUIAbstract, IGameStateFightGUI
    {
        private const string Angle = "\u00B0";
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
        
        private IHeroModel _heroModel;
        private IUpdateProvider _updateProvider;
        private ILaserFactoryInfo _laserFactoryInfo;
        public Button Continue => _continueButton;
        public Button Quite => _quiteButton;
        
        public void Init(IHeroModel heroModel, IUpdateProvider updateProvider, ILaserFactoryInfo laserFactoryInfo)
        {
            _heroModel = heroModel;
            _laserFactoryInfo = laserFactoryInfo;
            _updateProvider = updateProvider;
            _updateProvider.OnLateUpdate += UpdateGUI;
        }

        private void UpdateGUI()
        {
            _coordinate.text = $"X:{_heroModel.View.transform.position.x:0.00}, Y:{_heroModel.View.transform.position.x:0.00}";;
            _rotation.text = _heroModel.View.transform.eulerAngles.z.ToString("0") + Angle;
            _speed.text = _heroModel.Speed.ToString("0.00");
            _laserAmount.text = _laserFactoryInfo.Amount.ToString();
            _laserCooldown.text = _laserFactoryInfo.RespawnTime.ToString("0.0");
        }
        
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
            _updateProvider.OnLateUpdate -= UpdateGUI;
            GameObject.Destroy(gameObject);
        }
    }
}