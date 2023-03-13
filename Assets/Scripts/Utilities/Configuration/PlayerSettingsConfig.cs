using UnityEngine;

namespace Utilities.Configuration
{
    [CreateAssetMenu(fileName = "PlayerSettingsConfig", menuName = "Configuration/Settings/new PlayerSettingsConfig")]
    internal sealed class PlayerSettingsConfig : ScriptableObject
    {
        [SerializeField]
        private int harvestStackSize;

        internal int HarvestStackSize { get => harvestStackSize; }
    }
}