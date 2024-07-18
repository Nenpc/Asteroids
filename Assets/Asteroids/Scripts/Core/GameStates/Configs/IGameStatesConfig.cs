using Asteroids.Scripts.Core.Enums;

namespace Asteroids.Scripts.Core.GamesState.Configs
{
    public interface IGameStatesConfig
    {
        GameStateGUIAbstract GetGameStateConfigView(GameStates gameStatesType);
    }
}