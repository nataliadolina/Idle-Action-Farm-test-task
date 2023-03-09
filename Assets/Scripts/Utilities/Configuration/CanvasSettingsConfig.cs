using UnityEngine;

namespace Utilities.Configuration
{
    [CreateAssetMenu(fileName = "CanvasSettingsConfig", menuName = "Configuration/Settings/new CanvasSettingsConfig")]
    internal sealed class CanvasSettingsConfig : ScriptableObject
    {
        [SerializeField] private Vector2 canvasReferenceResolution;

        internal Vector2 CanvasReferenceResolution => canvasReferenceResolution;
    }
}
