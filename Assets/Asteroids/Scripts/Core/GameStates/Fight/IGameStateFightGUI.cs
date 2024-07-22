using Asteroids.Scripts.Core.Hero.Models;
using Asteroids.Scripts.Core.Weapons.Factories;
using Asteroids.Scripts.Infrastructure.UpdateProvider;

namespace Asteroids.Scripts.Core.GamesState.Fight
{
    public interface IGameStateFightGUI : IGameStateGUI
    {
        void Init(IHeroModel heroModel, IUpdateProvider updateProvider, ILaserFactoryInfo laserFactoryInfo);
    }
}