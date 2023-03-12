using UnityEngine;
using Zenject;
using Utilities.Configuration;

namespace DI.Installers
{
    internal class ConfigurationInstaller : MonoInstaller
    {
        [SerializeField] private CanvasSettingsConfig canvasSettingsConfig;
        [SerializeField] private PlayerSettingsConfig playerSettingsConfig;

        public override void InstallBindings()
        {
            Container.Bind<CanvasSettingsConfig>()
                .FromInstance(canvasSettingsConfig)
                .AsSingle()
                .NonLazy();
            Container.Bind<PlayerSettingsConfig>()
                .FromInstance(playerSettingsConfig)
                .AsSingle()
                .NonLazy();
        }
    }
}