using UnityEngine;
using Zenject;
using Systems;

namespace DI.Installers
{
    internal sealed class GlobalSystemsInstaller : MonoInstaller
    {
        [SerializeField] private TouchInputSystem touchInputSystem;
        [SerializeField] private HarvestContainer harvestContainer;
        
        public override void InstallBindings()
        {
            Container.Bind<TouchInputSystem>().FromInstance(touchInputSystem).AsSingle();
            Container.QueueForInject(touchInputSystem);

            Container.Bind<HarvestContainer>().FromInstance(harvestContainer).AsSingle();
            Container.QueueForInject(harvestContainer);
        }
    }
}