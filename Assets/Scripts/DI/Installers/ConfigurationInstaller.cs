using UnityEngine;
using Zenject;
using Utilities.Configuration;

namespace DI
{
    internal class ConfigurationInstaller : MonoInstaller
    {
        [SerializeField] private CanvasSettingsConfig canvasSettingsConfig;

        public override void InstallBindings()
        {
            Container.Bind<CanvasSettingsConfig>()
                .FromInstance(canvasSettingsConfig)
                .AsSingle()
                .NonLazy();
        }
    }
}