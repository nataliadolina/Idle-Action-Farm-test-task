using UnityEngine;
using Zenject;
using UI;
using UI.Interfaces;

namespace DI.Installers
{
    internal sealed class UIInstaller : MonoInstaller
    {
        [SerializeField] private PlayerDirectionInput playerDirectionInput;
        [SerializeField] private CutInput cutInput;

        public override void InstallBindings()
        {
            Container.Bind<IPlayerDirectionInput>().FromInstance(playerDirectionInput).AsSingle();
            Container.QueueForInject(playerDirectionInput);

            Container.Bind<ICutInput>().FromInstance(cutInput).AsSingle();
            Container.QueueForInject(cutInput);
        }
    }
}