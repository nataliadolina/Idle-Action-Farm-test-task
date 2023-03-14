using Zenject;

namespace DI
{
    internal class ContainerRef
    {
        private static DiContainer container;

        internal static DiContainer Container
        {
            get => container;
            set => container ??= value;
        }
    }
}
