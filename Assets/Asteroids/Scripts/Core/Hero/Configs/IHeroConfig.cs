using Asteroids.Scripts.Core.Hero.View;

namespace Asteroids.Scripts.Core.Hero.Configs
{
    public interface IHeroConfig
    {
        float Acceleration { get; }
        float MaxSpeed { get; }
        float Braking { get; }
        HeroView HeroView { get; }
    }
}