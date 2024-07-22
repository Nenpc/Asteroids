using Asteroids.Scripts.Core.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids.Scripts.Core.GamesState.Menu
{
    public sealed class GameStateMenuGUI : GameStateGUIAbstract, IGameStateMenuGUI
    {
        [SerializeField] private Button _startGame;
        [SerializeField] private Button _quit;
        
        public override GameStates States => GameStates.Menu;

        public Button StartGame => _startGame;
        public Button Quit => _quit;
        
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