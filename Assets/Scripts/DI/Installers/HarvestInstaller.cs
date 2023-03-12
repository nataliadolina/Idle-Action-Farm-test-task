using Zenject;
using Environment;

namespace DI.Installers
{
    internal class HarvestInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            WheatBlock[] wheatBlocks = FindObjectsOfType<WheatBlock>(true);
            foreach (var instance in wheatBlocks)
            {
                Container.Bind<WheatBlock>().FromInstance(instance).AsCached();
                Container.QueueForInject(instance);
            }
        }
    }
}