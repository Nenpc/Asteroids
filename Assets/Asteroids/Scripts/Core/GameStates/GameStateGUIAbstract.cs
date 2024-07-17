using Asteroids.Scripts.Core.Enums;
using UnityEngine;

namespace Asteroids.Scripts.Core.GamesState
{
    public abstract class GameStateGUIAbstract : MonoBehaviour
    {
        public abstract GameStates States { get; }
    }
}