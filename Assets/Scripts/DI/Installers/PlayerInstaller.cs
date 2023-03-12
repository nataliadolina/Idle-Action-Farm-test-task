using UnityEngine;
using Zenject;
using Player;

namespace DI.Installers
{
    internal sealed class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerAnimator playerAnimator;
        [SerializeField] private PlayerCut playerCut;
        [SerializeField] private PlayerMovement playerMovement;

        public override void InstallBindings()
        {
            Container.Bind<PlayerAnimator>().FromInstance(playerAnimator).AsSingle().NonLazy();
            Container.Bind<PlayerCut>().FromInstance(playerCut).AsSingle().NonLazy();
            Container.Bind<PlayerMovement>().FromInstance(playerMovement).AsSingle().NonLazy();
        }
    }
}