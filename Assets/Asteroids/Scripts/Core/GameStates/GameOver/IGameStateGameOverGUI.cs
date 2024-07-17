﻿using UnityEngine.UI;

namespace Asteroids.Scripts.Core.GamesState.GameOver
{
    public interface IGameStateGameOverGUI : IGameStateGUIBase
    {
        public Button RestartGame { get; }
        public Button Quite { get; }
    }
}