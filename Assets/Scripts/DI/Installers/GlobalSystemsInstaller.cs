using UnityEngine;
using Zenject;
using Systems;
using Player;
using System.ComponentModel;
using Environment;

namespace DI.Installers
{
    internal sealed class GlobalSystemsInstaller : MonoInstaller
    {
        [SerializeField] private TouchInputSystem touchInputSystem;
        [SerializeField] private HarvestColliderCutReceiversContainer harvestColliderCutReceiversContainer;
        [SerializeField] private HarvestStack harvestStack;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private SellManager sellManager;

        public override void InstallBindings()
        {
            Container.Bind<PlayerMovement>().FromInstance(playerMovement).AsSingle();
            Container.QueueForInject(playerMovement);

            Container.Bind<HarvestStack>().FromInstance(harvestStack).AsSingle();
            Container.QueueForInject(harvestColliderCutReceiversContainer);

            Container.Bind<TouchInputSystem>().FromInstance(touchInputSystem).AsSingle();
            Container.QueueForInject(touchInputSystem);

            Container.Bind<HarvestColliderCutReceiversContainer>().FromInstance(harvestColliderCutReceiversContainer).AsSingle();
            Container.QueueForInject(harvestColliderCutReceiversContainer);

            Container.Bind<SellManager>().FromInstance(sellManager).AsSingle();
            Container.QueueForInject(sellManager);
        }
    }
}