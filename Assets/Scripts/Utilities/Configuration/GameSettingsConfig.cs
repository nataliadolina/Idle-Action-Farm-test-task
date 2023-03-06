using UnityEngine;

namespace Utilities.Configuration
{
    [CreateAssetMenu(fileName = "GameSettingsConfig", menuName = "Configuration/GameSettings/new GameSettingsConfig")]
    internal class GameSettingsConfig : ScriptableObject
    {
        internal const int FPS = 60;
        internal const float UPDATE_RATE = 1 / 60f;
    }
}
