using Asteroids.Scripts.Core.Enemies.Config;
using Asteroids.Scripts.Core.GamesState.Configs;
using UnityEngine;
using Zenject;
using Asteroids.Scripts.Core.Hero.Configs;
using Asteroids.Scripts.Core.Weapons.Config;

namespace Asteroids.Core.Initialize
{
    [CreateAssetMenu(menuName = "Asteroids/Config Installer")]
    public sealed class ConfigInstaller : ScriptableObjectInstaller<ConfigInstaller>
    {
        [SerializeField] private WeaponsConfig _weaponsConfig;
        [SerializeField] private HeroConfig _heroConfig;
        [SerializeField] private EnemiesConfig _enemiesConfig;
        [SerializeField] private GameStatesConfig _gameStatesConfig;

        private void InstallCurrency()
        {
            Container.Bind<IHeroConfig>().FromInstance(_heroConfig);
            Container.Bind<IWeaponsConfig>().FromInstance(_weaponsConfig);
            Container.Bind<IEnemiesConfig>().FromInstance(_enemiesConfig);
            Container.Bind<IGameStatesConfig>().FromInstance(_gameStatesConfig);
        }

        public override void InstallBindings()
        {
            InstallCurrency();
        }
    }
}