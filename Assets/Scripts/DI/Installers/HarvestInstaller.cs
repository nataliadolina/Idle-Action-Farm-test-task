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

            Wheat[] wheat = FindObjectsOfType<Wheat>(true);
            foreach (var instance in wheat)
            {
                Container.Bind<Wheat>().FromInstance(instance).AsCached();
                Container.QueueForInject(instance);
            }

            WheatColliderCutReceiver[] cutReceivers = FindObjectsOfType<WheatColliderCutReceiver>(true);
            foreach (var instance in cutReceivers)
            {
                Container.Bind<WheatColliderCutReceiver>().FromInstance(instance).AsCached();
                Container.QueueForInject(instance);
            }
        }
    }
}