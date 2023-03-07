using UnityEngine;
using Zenject;
using UI;
using UI.Interfaces;

namespace DI
{
    internal sealed class UIInstaller : MonoInstaller
    {
        [SerializeField] private PlayerDirectionInput playerDirectionInput;

        public override void InstallBindings()
        {
            Container.Bind<IPlayerDirectionInput>().FromInstance(playerDirectionInput).AsSingle();
            Container.QueueForInject(playerDirectionInput);
        }
    }
}