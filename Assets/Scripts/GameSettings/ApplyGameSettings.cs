using UnityEngine;
using Utilities.Configuration;

namespace GameSettings
{
    internal class ApplyGameSettings : MonoBehaviour
    {
        void Start()
        {
            Application.targetFrameRate = GameSettingsConfig.FPS;
        }
    }
}
