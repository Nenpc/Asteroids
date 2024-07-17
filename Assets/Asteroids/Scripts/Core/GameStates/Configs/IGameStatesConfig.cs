using Asteroids.Scripts.Core.Enums;

namespace Asteroids.Scripts.Core.GamesState.Configs
{
    public interface IGameStatesConfig
    {
        GameStateConfig GetGameStateConfigView(GameStates gameStatesType);
    }
}