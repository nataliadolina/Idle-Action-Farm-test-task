using UnityEngine;
using Zenject;
using Systems;

namespace DI.Installers
{
    internal sealed class GlobalSystemsInstaller : MonoInstaller
    {
        [SerializeField] private TouchInputSystem touchInputSystem;
        [SerializeField] private HarvestColliderCutReceiversContainer harvestColliderCutReceiversContainer;
        
        public override void InstallBindings()
        {
            Container.Bind<TouchInputSystem>().FromInstance(touchInputSystem).AsSingle();
            Container.QueueForInject(touchInputSystem);

            Container.Bind<HarvestColliderCutReceiversContainer>().FromInstance(harvestColliderCutReceiversContainer).AsSingle();
            Container.QueueForInject(harvestColliderCutReceiversContainer);
        }
    }
}