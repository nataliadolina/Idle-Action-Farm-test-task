using UnityEngine;
using Zenject;
using Systems;

namespace DI
{
    internal sealed class GlobalSystemsInstaller : MonoInstaller
    {
        [SerializeField] private TouchInputSystem touchInputSystem;

        public override void InstallBindings()
        {
            Container.Bind<TouchInputSystem>().FromInstance(touchInputSystem).AsSingle();
            Container.QueueForInject(touchInputSystem);
        }
    }
}