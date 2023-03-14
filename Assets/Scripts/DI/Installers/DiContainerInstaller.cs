using Zenject;

namespace DI.Installers
{
    internal sealed class DiContainerInstaller : MonoInstaller
    {
        [Inject] private DiContainer diContainer;

        public override void InstallBindings()
        {
            ContainerRef.Container = diContainer;
        }
    }
}
